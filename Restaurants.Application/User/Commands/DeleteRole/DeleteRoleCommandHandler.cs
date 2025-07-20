using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler(ILogger<DeleteRoleCommandHandler> logger,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<DeleteRoleCommand>
    {
        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByNameAsync(request.RoleName) ??
                throw new BadRequestException("Role not found");

            var result = await roleManager.DeleteAsync(role);

            logger.LogInformation("Delete Role {Role}", request.RoleName);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to delete role: {errors}");
            }
        }
    }
}
