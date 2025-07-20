using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.DeleteRole
{
    public class DeleteRoleCommand(string roleName) : IRequest
    {
        [DefaultValue("User")]
        public string RoleName { get; } = roleName;
    }
}
