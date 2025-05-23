using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class Dish
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(255)]
        public string? ImageFileName { get; set; }

        // Navigation Property

        [Required]
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; } = default!;

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = default!;

        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
