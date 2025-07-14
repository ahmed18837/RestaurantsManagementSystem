using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using System.Data;

namespace Restaurants.Application.User.Commands.AddRole
{
    public class AddRoleCommandHandler(ILogger<AddRoleCommandHandler> logger,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<AddRoleCommand>
    {
        public async Task Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            if (await roleManager.RoleExistsAsync(request.RoleName))
                throw new DuplicateNameException($"Role {request.RoleName} already exists.");

            var concurrencyStamp = Guid.NewGuid().ToString();

            var role = new IdentityRole
            {
                Id = concurrencyStamp,
                Name = request.RoleName,
                NormalizedName = request.RoleName.ToUpper(),
                ConcurrencyStamp = concurrencyStamp
            };

            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Failed to create role: {errors}");
            }

            logger.LogInformation("Role {RoleName} created successfully", request.RoleName);
        }
    }
}
