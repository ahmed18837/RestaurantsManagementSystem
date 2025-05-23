using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; }

        // Navigation Property

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = default!;

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
