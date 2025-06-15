using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories.GenericRepository;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    public class CategoryRepository(RestaurantsDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
    {
        //public async Task<int> Create(Category entity)
        //{
        //    dbContext.Categories.Add(entity);
        //    await dbContext.SaveChangesAsync();
        //    return entity.Id;
        //}

        //public async Task Delete(Category entity)
        //{
        //    dbContext.Remove(entity);
        //    await dbContext.SaveChangesAsync();
        //}

        //public async Task<Category?> GetByIdAsync(int id)
        //{
        //    var category = await dbContext.Categories
        //       .FirstOrDefaultAsync(d => d.Id == id);

        //    return category;
        //}

        public async Task<(IEnumerable<Category>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext
                .Categories
                .Where(d => searchPhraseLower == null || (d.Name.ToLower().Contains(searchPhraseLower)
                                                       || d.Description!.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Category, object>>>
            {
                { nameof(Category.Name), d => d.Name },
                { nameof(Category.Description), d => d.Description! },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var categories = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
            .ToListAsync();

            return (categories, totalCount);
        }

    }
}
