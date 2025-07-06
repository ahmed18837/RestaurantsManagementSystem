using MediatR;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQuery(int customerId) : IRequest<CustomerOrdersDto>
    {
        public int CustomerId { get; } = customerId;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
