using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories.GenericRepository;

namespace Restaurants.Domain.Repositories
{
    public interface ICustomersRepository : IGenericRepository<Customer>
    {
        Task<(IEnumerable<Customer>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        Task<Customer?> GetByEmailAsync(string email);
        Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
        Task AddFavoriteRestaurantAsync(int customerId, int restaurantId);
        Task<List<Restaurant>> GetFavoriteRestaurantsAsync(int customerId);

    }
}
