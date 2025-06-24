using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.GenericRepository;


namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository : IGenericRepository<Restaurant>
    {
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        Task<List<string>> GetCategoriesForRestaurantAsync(int restaurantId);
        Task<List<Restaurant>> GetTopRatedAsync(int count);
        Task<Restaurant?> GetStatisticsAsync(int restaurantId);
        Task<int> GetTotalOrdersForRestaurantAsync(int restaurantId);
        Task<decimal> GetTotalRevenueForRestaurantAsync(int restaurantId);
        Task<string?> GetMostPopularDishNameAsync(int restaurantId);
        Task<DateTime?> GetLastOrderDateAsync(int restaurantId);
    }
}
