using MediatR;
using Restaurants.Application.User.Dtos;

namespace Restaurants.Application.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery(string email) : IRequest<UserDto>
    {
        public string Email { get; } = email;
    }
}
