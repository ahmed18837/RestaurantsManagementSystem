using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.RegisterUser
{
    internal class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IMapper mapper) : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new User {@User}", request);

            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new BadRequestException("A user with this email already exists");

            var appUser = mapper.Map<ApplicationUser>(request);

            var identityResult = await userManager.CreateAsync(appUser, request.Password);
            if (!identityResult.Succeeded)
            {
                var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                throw new BadRequestException($"User creation failed: {errors}");
            }

            identityResult = await userManager.AddToRoleAsync(appUser, request.UserType);
            if (!identityResult.Succeeded)
            {
                var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to assign the role 'User': {errors}");
            }

            return identityResult;
        }
    }
}
