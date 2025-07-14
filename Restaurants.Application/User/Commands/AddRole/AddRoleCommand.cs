using MediatR;

namespace Restaurants.Application.User.Commands.AddRole
{
    public class AddRoleCommand(string roleName) : IRequest
    {
        public string RoleName { get; } = roleName;
    }
}
