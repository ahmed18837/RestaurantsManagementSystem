using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System.Data;

namespace Restaurants.Application.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IMapper mapper) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

            //if (user.FullName == request.FullName && user.PhoneNumber == request.PhoneNumber
            //    && user.Email == request.Email)
            //    throw new BadRequestException("No changes detected. The data is already up to date");

            var emailOwner = await userManager.FindByEmailAsync(request.Email);
            if (emailOwner is not null && emailOwner.Id != user.Id)
                throw new DuplicateNameException($"This Email : {request.Email} is already used by another account");

            var phoneOwner = await userManager.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber && u.Id != user.Id, cancellationToken);
            if (phoneOwner is not null)
                throw new DuplicateNameException($"This phone number : {request.PhoneNumber} is already used by another account");

            mapper.Map(request, user);

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to update user: {errors}");
            }

            logger.LogInformation("User {UserId} successfully updated.", user.Id);
        }
    }
}
