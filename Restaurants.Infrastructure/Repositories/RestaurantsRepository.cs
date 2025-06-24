using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories.GenericRepository;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantsRepository(RestaurantsDbContext dbContext) : GenericRepository<Restaurant>(dbContext), IRestaurantsRepository
    {
        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase,
             int pageSize,
             int pageNumber,
             string? sortBy,
             SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext
                .Restaurants
                .AsNoTracking()
                .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                       || r.Description.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
        }

        public async Task<List<string>> GetCategoriesForRestaurantAsync(int restaurantId)
        {
            return await dbContext.Dishes
                .Where(d => d.RestaurantId == restaurantId)
                .Select(d => d.Category.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Restaurant>> GetTopRatedAsync(int count)
        {
            return await dbContext.Restaurants
                .Include(r => r.Ratings)
                .OrderByDescending(r => r.Ratings.Average(r => r.Stars))
                .Take(count)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetStatisticsAsync(int restaurantId)
        {
            return await dbContext.Restaurants
                .Include(r => r.Ratings)
                .Include(r => r.Dishes)
                .ThenInclude(d => d.OrderItems)
                .FirstOrDefaultAsync(r => r.Id == restaurantId);
        }

        public async Task<int> GetTotalOrdersForRestaurantAsync(int restaurantId)
        {
            return await dbContext.OrderItems
                .Where(oi => oi.Dish.RestaurantId == restaurantId)
                .Select(oi => oi.OrderId)
                .Distinct()
                .CountAsync();
        }

        public async Task<decimal> GetTotalRevenueForRestaurantAsync(int restaurantId)
        {
            return await dbContext.OrderItems
                .Where(oi => oi.Dish.RestaurantId == restaurantId)
                .SumAsync(oi => oi.UnitPrice * oi.Quantity);
        }

        public async Task<string?> GetMostPopularDishNameAsync(int restaurantId)
        {
            return await dbContext.OrderItems
                .Where(oi => oi.Dish.RestaurantId == restaurantId)
                .GroupBy(oi => new { oi.DishId, oi.Dish.Name })
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Select(g => g.Key.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<DateTime?> GetLastOrderDateAsync(int restaurantId)
        {
            return await dbContext.OrderItems
                .Where(oi => oi.Dish.RestaurantId == restaurantId)
                .Select(oi => oi.Order.OrderDate)
                .OrderByDescending(date => date)
                .FirstOrDefaultAsync();
        }

    }
}
