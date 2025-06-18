using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.GenericRepository;

namespace Restaurants.Domain.Repositories
{
    public interface IRatingsRepository : IGenericRepository<Rating>
    {
        Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetAllMatchingAsync(
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection);
        Task<Rating?> GetByIdWithIncluded(int id);
    }
}
