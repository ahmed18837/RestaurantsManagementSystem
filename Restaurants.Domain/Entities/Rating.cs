using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = default!;

        [Required]
        public int DishId { get; set; }

        public Dish Dish { get; set; } = default!;

        public int? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
    }
}
