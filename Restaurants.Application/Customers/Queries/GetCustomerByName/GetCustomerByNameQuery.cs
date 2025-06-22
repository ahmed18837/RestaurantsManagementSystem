using MediatR;
using Restaurants.Application.Customers.Dtos;

namespace Restaurants.Application.Customers.Queries.GetCustomerByName
{
    public class GetCustomerByNameQuery(string name) : IRequest<CustomerDto>
    {
        public string Name { get; } = name;
    }
}
