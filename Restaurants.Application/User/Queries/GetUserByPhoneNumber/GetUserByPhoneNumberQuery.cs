using MediatR;
using Restaurants.Application.User.Dtos;

namespace Restaurants.Application.User.Queries.GetUserByPhoneNumber
{
    public class GetUserByPhoneNumberQuery(string phoneNumber) : IRequest<UserDto>
    {
        public string PhoneNumber { get; } = phoneNumber;
    }
}
