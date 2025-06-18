namespace Restaurants.Application.Ratings.Dtos
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;
        public int DishId { get; set; }
        public string DishName { get; set; } = default!;
        public int? RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
    }
}
