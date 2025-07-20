using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Queries.GetRolesByEmail
{
    public class GetRolesByEmailQueryHandler(ILogger<GetRolesByEmailQueryHandler> logger,
        UserManager<ApplicationUser> userManager) : IRequestHandler<GetRolesByEmailQuery, IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> Handle(GetRolesByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                 ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

            var roles = await userManager.GetRolesAsync(user);

            logger.LogInformation("Get Role For Email : {Email}", request.Email);

            return roles;
        }
    }
}
