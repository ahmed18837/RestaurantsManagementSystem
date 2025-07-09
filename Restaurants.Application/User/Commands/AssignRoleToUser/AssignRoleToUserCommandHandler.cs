using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System.Data;

namespace Restaurants.Application.User.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandler(ILogger<AssignRoleToUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignRoleToUserCommand, string>
    {
        public async Task<string> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assign a Role {UserType} To {Email}", request.UserType, request.Email);

            var existingUser = await userManager.FindByEmailAsync(request.Email)
               ?? throw new NotFoundException(nameof(ApplicationUser), request.Email.ToString());

            if (!await roleManager.RoleExistsAsync(request.UserType))
                throw new NotFoundException(nameof(ApplicationUser), request.UserType.ToString());

            if (await userManager.IsInRoleAsync(existingUser, request.UserType))
            {
                throw new DuplicateNameException($"This Email : {request.Email} already existing in Role {request.UserType}");
            }

            var result = await userManager.AddToRoleAsync(existingUser, request.UserType);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to assign role: {errors}");
            }

            return $"Role '{request.UserType}' assigned to user '{request.Email}' successfully";

        }
    }
}
