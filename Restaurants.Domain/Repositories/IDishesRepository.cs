using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.GenericRepository;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository : IGenericRepository<Dish>
    {
        Task<(IEnumerable<Dish>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        Task Delete(IEnumerable<Dish> entities);
    }


}
