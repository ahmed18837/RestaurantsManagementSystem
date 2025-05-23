using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        // Navigation Property

        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; } = default!;

        [Required]
        public int DishId { get; set; }

        public Dish Dish { get; set; } = default!;
    }
}
