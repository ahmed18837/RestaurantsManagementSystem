namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantStatisticsDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = default!;
        public double AverageRating { get; set; }
        public int DishesCount { get; set; }
        public int TotalOrders { get; set; }
        public int TotalRatings { get; set; }
        public string? MostPopularDishName { get; set; }
        public decimal TotalRevenue { get; set; }
        public DateTime? LastOrderDate { get; set; }
    }
}
