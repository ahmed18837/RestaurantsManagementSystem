using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.GenericRepository;

namespace Restaurants.Domain.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<(IEnumerable<Category>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        //Task<Category?> GetByIdAsync(int id);
        //Task<int> Create(Category entity);
        //Task Delete(Category entity);
    }
}
