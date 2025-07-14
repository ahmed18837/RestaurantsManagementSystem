using MediatR;

namespace Restaurants.Application.User.Commands.DeleteUser
{
    public class DeleteUserCommand(string id) : IRequest
    {
        public string Id { get; } = id!;
    }
}
