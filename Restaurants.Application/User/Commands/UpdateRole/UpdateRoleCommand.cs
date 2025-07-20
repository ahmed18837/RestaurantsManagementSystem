using MediatR;

namespace Restaurants.Application.User.Commands.UpdateRole
{
    public class UpdateRoleCommand(string oldRoleName, string newRoleName) : IRequest
    {
        public string OldRoleName { get; } = oldRoleName;
        public string NewRoleName { get; } = newRoleName;
    }
}
