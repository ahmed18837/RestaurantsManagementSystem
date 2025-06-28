using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;

        [EmailAddress, MaxLength(100)]
        public string Email { get; set; } = default!;

        [Phone, MaxLength(15)]
        public string PhoneNumber { get; set; } = default!;

        // Navigation Property

        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<Restaurant> FavoriteRestaurants { get; set; } = [];


        // ✅ الربط مع جدول الهوية (IdentityUser)
        //[Required]
        //public string ApplicationUserId { get; set; } = default!;

        //public ApplicationUser ApplicationUser { get; set; } = default!;
    }
}
