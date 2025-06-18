using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Customers.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<PagedResult<CustomerDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
