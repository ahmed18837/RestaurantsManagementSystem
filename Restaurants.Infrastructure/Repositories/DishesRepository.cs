using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(IEnumerable<Dish> entities)
        {
            dbContext.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Dish>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext
                .Dishes
                .Where(d => searchPhraseLower == null || (d.Name.ToLower().Contains(searchPhraseLower)
                                                       || d.Description!.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Dish, object>>>
            {
                { nameof(Dish.Name), d => d.Name },
                { nameof(Dish.Description), d => d.Description! },
                { nameof(Dish.Price), d => d.Price },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var dishes = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (dishes, totalCount);
        }

        public async Task<Dish?> GetByIdAsync(int id)
        {
            var dish = await dbContext.Dishes
               .FirstOrDefaultAsync(d => d.Id == id);

            return dish;
        }
    }
}
