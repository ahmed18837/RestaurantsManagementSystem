using Restaurants.Application.Common;
using Restaurants.Application.Customers.Dtos;

namespace Restaurants.Application.Orders.Dtos
{
    public class CustomerOrdersDto
    {
        public CustomerDto Customer { get; set; } = default!;
        public PagedResult<OrderDto> OrdersPaged { get; set; } = default!;
    }

}
