using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;

        [MaxLength(500)]
        public string Description { get; set; } = default!;

        public bool HasDelivery { get; set; }

        [EmailAddress, MaxLength(100)]
        public string? ContactEmail { get; set; }

        [Phone, MaxLength(15)]
        public string? ContactNumber { get; set; }

        public ApplicationUser? Owner { get; set; } = default!;
        public string? OwnerId { get; set; } = default!;

        // Owned Entity
        public Address? Address { get; set; }

        // Navigation Property
        public ICollection<Dish> Dishes { get; set; } = [];

        public ICollection<Rating> Ratings { get; set; } = [];
    }
}
