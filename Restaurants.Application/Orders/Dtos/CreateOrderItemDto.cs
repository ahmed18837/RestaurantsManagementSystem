using System.ComponentModel;

namespace Restaurants.Application.Orders.Dtos
{
    public class CreateOrderItemDto
    {
        [DefaultValue(620)]
        public int DishId { get; set; }

        [DefaultValue(2)]
        public int Quantity { get; set; }
    }

}
