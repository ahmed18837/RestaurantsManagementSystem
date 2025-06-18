using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.GenericRepository;

namespace Restaurants.Domain.Repositories
{
    public interface IOrdersRepository : IGenericRepository<Order>
    {
        Task<(IEnumerable<Order>, int)> GetAllMatchingAsync(int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        Task<Order?> GetByIdIncludeWithOrderItemsAsync(int id);
    }
}
