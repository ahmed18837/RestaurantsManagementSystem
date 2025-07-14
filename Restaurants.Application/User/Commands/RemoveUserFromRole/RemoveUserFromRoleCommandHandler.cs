using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.RemoveUserFromRole
{
    public class RemoveUserFromRoleCommandHandler(ILogger<RemoveUserFromRoleCommandHandler> logger,
        UserManager<ApplicationUser> userManager) : IRequestHandler<RemoveUserFromRoleCommand, string>
    {
        public async Task<string> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.UserType))
                return "Email and role are required.";

            var user = await userManager.FindByEmailAsync(request.Email)
               ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

            if (!await userManager.IsInRoleAsync(user, request.UserType))
                throw new BadRequestException("User is not in this Role!");

            var result = await userManager.RemoveFromRoleAsync(user, request.UserType);
            if (!result.Succeeded)
                throw new BadRequestException("Failed to remove user from role");

            logger.LogInformation("Remove a User with email : {Email} From {UserType}", request.Email, request.UserType);

            return $"User with email : {request.Email} removed from role {request.UserType} successfully.";
        }
    }
}
