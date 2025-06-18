using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
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
                .Where(d => searchPhraseLower == null || (d.FullName.ToLower().Contains(searchPhraseLower)
                                                       || d.Email!.ToLower().Contains(searchPhraseLower))
                                                       || d.PhoneNumber!.ToLower().Contains(searchPhraseLower));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Customer, object>>>
            {
                { nameof(Customer.FullName), d => d.FullName },
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
    }
}
