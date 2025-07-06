using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories.GenericRepository;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepository(RestaurantsDbContext dbContext) : GenericRepository<Dish>(dbContext), IDishesRepository
    {
        //public async Task<int> Create(Dish entity)
        //{
        //    dbContext.Dishes.Add(entity);
        //    await dbContext.SaveChangesAsync();
        //    return entity.Id;
        //}       

        //public async Task<Dish?> GetByIdAsync(int id)
        //{
        //    var dish = await dbContext.Dishes
        //       .FirstOrDefaultAsync(d => d.Id == id);

        //    return dish;
        //}

        public async Task<(IEnumerable<Dish> Dishes, int TotalCount)> GetAllMatchingAsync(
             int restaurantId,
       string? searchPhrase,
       int pageSize,
       int pageNumber,
       string? sortBy,
       SortDirection sortDirection)
        {

            // 1) تجهيز عبارة البحث
            string? term = string.IsNullOrWhiteSpace(searchPhrase)
                ? null
                : searchPhrase.Trim().ToLower();

            // 2) الاستعلام الأساسى مع Includes صحيحة
            IQueryable<Dish> query = dbContext.Dishes.AsNoTracking()
                .Include(d => d.Restaurant)
                .Include(d => d.Category)
                .Where(d => d.RestaurantId == restaurantId)
                .Where(d =>
                    term == null ||
                    EF.Functions.Like(d.Name.ToLower(), $"%{term}%") ||
                    EF.Functions.Like((d.Description ?? "").ToLower(), $"%{term}%"));

            int totalCount = await query.CountAsync();

            // 3) ترتيب ديناميكى آمن
            var columns = new Dictionary<string, Expression<Func<Dish, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(Dish.Name)] = d => d.Name,
                [nameof(Dish.Description)] = d => d.Description!,
                [nameof(Dish.Price)] = d => d.Price
            };

            if (!string.IsNullOrEmpty(sortBy) && columns.TryGetValue(sortBy, out var column))
            {
                query = sortDirection == SortDirection.Ascending
                      ? query.OrderBy(column)
                      : query.OrderByDescending(column);
            }

            // 4) Paging
            List<Dish> dishes = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (dishes, totalCount);
        }

        public async Task Delete(IEnumerable<Dish> entities)
        {
            dbContext.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Dish?> GetByIdIncludeRestaurantAndCategory(int id)
        {
            var dish = await dbContext.Dishes
                .AsNoTracking()
                .Include(d => d.Restaurant)
                .Include(d => d.Category)
                .FirstOrDefaultAsync(d => d.Id == id);

            return dish;
        }
    }
}
