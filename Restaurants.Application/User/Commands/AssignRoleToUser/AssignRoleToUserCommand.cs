using MediatR;
using System.ComponentModel;

namespace Restaurants.Application.User.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommand : IRequest<string>
    {
        [DefaultValue("user@gmail.com")]
        public string Email { get; set; } = default!;

        [DefaultValue("Admin")]
        public string UserType { get; set; } = default!;
    }
}
