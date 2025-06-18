using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.GenericRepository;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository : IGenericRepository<Dish>
    {
        Task<(IEnumerable<Dish> Dishes, int TotalCount)> GetAllMatchingAsync(
           string? searchPhrase,
           int pageSize,
           int pageNumber,
           string? sortBy,
           SortDirection sortDirection);
        Task<Dish?> GetByIdIncludeRestaurantAndCategory(int id);
        Task Delete(IEnumerable<Dish> entities);
    }


}
