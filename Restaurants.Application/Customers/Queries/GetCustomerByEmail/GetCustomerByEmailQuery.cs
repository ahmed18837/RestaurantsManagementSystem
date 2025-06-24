using MediatR;
using Restaurants.Application.Customers.Dtos;

namespace Restaurants.Application.Customers.Queries.GetCustomerByEmail
{
    public class GetCustomerByEmailQuery(string email) : IRequest<CustomerDto>
    {
        public string Email { get; } = email;
    }
}
