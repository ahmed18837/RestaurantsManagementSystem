using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Orders.Dtos
{
    public class RestaurantOrdersDto
    {
        public RestaurantDto Restaurant { get; set; } = default!;
        public PagedResult<OrderDto> OrdersPaged { get; set; } = default!;
    }
}
