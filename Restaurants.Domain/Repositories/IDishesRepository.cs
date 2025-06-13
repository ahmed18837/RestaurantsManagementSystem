using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<(IEnumerable<Dish>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        Task<Dish?> GetByIdAsync(int id);
        Task<int> Create(Dish entity);
        Task Delete(IEnumerable<Dish> entities);
    }


}
