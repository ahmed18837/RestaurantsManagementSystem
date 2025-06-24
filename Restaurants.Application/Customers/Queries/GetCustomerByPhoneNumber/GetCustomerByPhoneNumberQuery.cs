using MediatR;
using Restaurants.Application.Customers.Dtos;

namespace Restaurants.Application.Customers.Queries.GetCustomerByPhoneNumber
{
    public class GetCustomerByPhoneNumberQuery(string phoneNumber) : IRequest<CustomerDto>
    {
        public string PhoneNumber { get; } = phoneNumber;
    }
}
