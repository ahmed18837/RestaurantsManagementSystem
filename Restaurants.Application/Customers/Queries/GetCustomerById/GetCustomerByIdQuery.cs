using MediatR;
using Restaurants.Application.Customers.Dtos;

namespace Restaurants.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery(int id) : IRequest<CustomerDto>
    {
        public int Id { get; } = id;
    }
}
