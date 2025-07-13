using MediatR;
using Restaurants.Application.User.Dtos;

namespace Restaurants.Application.User.Queries.GetUserById
{
    public class GetUserByIdQuery(string id) : IRequest<UserDto>
    {
        public string Id { get; } = id;
    }
}
