using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler(ILogger<UpdateRoleCommandHandler> logger,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<UpdateRoleCommand>
    {
        public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByNameAsync(request.OldRoleName) ??
                throw new BadRequestException("Role not found");

            var roleExists = await roleManager.RoleExistsAsync(request.NewRoleName);
            if (roleExists)
                throw new BadRequestException("New role name already exists");

            role.Name = request.NewRoleName;

            var result = await roleManager.UpdateAsync(role);

            logger.LogInformation("Update Old Role {OldRoleName} To New Role {NewRoleName}", request.OldRoleName, request.NewRoleName);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to update role: {errors}");
            }
        }
    }
}
