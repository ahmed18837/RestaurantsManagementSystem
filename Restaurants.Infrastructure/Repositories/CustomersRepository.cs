using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories.GenericRepository;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    public class CustomersRepository(RestaurantsDbContext dbContext) : GenericRepository<Customer>(dbContext), ICustomersRepository
    {
        public async Task<(IEnumerable<Customer>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext
                .Customers
                .AsNoTracking()
                .Where(d => searchPhraseLower == null || (d.Name.ToLower().Contains(searchPhraseLower)
                                                       || d.Email!.ToLower().Contains(searchPhraseLower))
                                                       || d.PhoneNumber!.ToLower().Contains(searchPhraseLower));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Customer, object>>>
            {
                { nameof(Customer.Name), d => d.Name },
                { nameof(Customer.Email), d => d.Email! },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var customers = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .AsNoTracking()
            .ToListAsync();

            return (customers, totalCount);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            var customer = await dbContext.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);

            return customer;
        }

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
        {
            var customer = await dbContext.Customers
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);

            return customer;
        }

        public async Task AddFavoriteRestaurantAsync(int customerId, int restaurantId)
        {
            var customer = await dbContext.Customers
                .Include(c => c.FavoriteRestaurants)
                .FirstOrDefaultAsync(c => c.Id == customerId)
                ?? throw new NotFoundException(nameof(Customer), customerId.ToString());

            var restaurant = await dbContext.Restaurants.FindAsync(restaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), restaurantId.ToString());

            if (!customer.FavoriteRestaurants.Any(r => r.Id == restaurantId))
            {
                customer.FavoriteRestaurants.Add(restaurant);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Restaurant>> GetFavoriteRestaurantsAsync(int customerId)
        {
            var customer = await dbContext.Customers
                .Include(c => c.FavoriteRestaurants)
                .FirstOrDefaultAsync(c => c.Id == customerId)
                ?? throw new NotFoundException(nameof(Customer), customerId.ToString());

            return [.. customer.FavoriteRestaurants];
        }

        public async Task<Customer?> GetByIdWithFavoritesAsync(int id, CancellationToken ct = default)
        {
            return await dbContext.Customers
                                 .Include(c => c.FavoriteRestaurants)
                                 .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

    }
}
