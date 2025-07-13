using MediatR;
using Restaurants.Application.User.Dtos;

namespace Restaurants.Application.User.Queries.GetUserByName
{
    public class GetUserByNameQuery(string fullName) : IRequest<UserDto>
    {
        public string FullName { get; } = fullName;
    }
}
