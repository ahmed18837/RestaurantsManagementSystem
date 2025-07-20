using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.User.Commands.RegisterMultipleUsers
{
    internal class RegisterMultipleUsersCommandHandler(
        ILogger<RegisterMultipleUsersCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IMapper mapper)
        : IRequestHandler<RegisterMultipleUsersCommand, List<IdentityResult>>
    {
        public async Task<List<IdentityResult>> Handle(RegisterMultipleUsersCommand request, CancellationToken cancellationToken)
        {
            var results = new List<IdentityResult>();

            foreach (var userCommand in request.Users)
            {
                logger.LogInformation("Creating user {@User}", userCommand);

                var existingUser = await userManager.FindByEmailAsync(userCommand.Email);
                if (existingUser != null)
                {
                    results.Add(IdentityResult.Failed(new IdentityError
                    {
                        Description = $"User with email {userCommand.Email} already exists."
                    }));
                    continue;
                }

                var appUser = mapper.Map<ApplicationUser>(userCommand);

                var identityResult = await userManager.CreateAsync(appUser, userCommand.Password);
                if (!identityResult.Succeeded)
                {
                    results.Add(IdentityResult.Failed(identityResult.Errors.ToArray()));
                    continue;
                }

                identityResult = await userManager.AddToRoleAsync(appUser, userCommand.UserType);
                if (!identityResult.Succeeded)
                {
                    results.Add(IdentityResult.Failed(identityResult.Errors.ToArray()));
                    continue;
                }

                results.Add(IdentityResult.Success);
            }

            return results;
        }
    }

}
