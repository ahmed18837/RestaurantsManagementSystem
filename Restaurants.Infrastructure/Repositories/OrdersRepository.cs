using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories.GenericRepository;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    public class OrdersRepository(RestaurantsDbContext dbContext) : GenericRepository<Order>(dbContext), IOrdersRepository
    {
        public async Task<(IEnumerable<Order>, int)> GetAllMatchingAsync(int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = dbContext
                .Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Dish)
                .AsNoTracking();

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Order, object>>>
            {
                { nameof(Order.TotalPrice), d => d.TotalPrice },
               // { nameof(Order.Email), d => d.Email! },
            };

                if (sortBy != null && columnsSelector.TryGetValue(sortBy, out var selectedColumn))
                {
                    baseQuery = (sortDirection == SortDirection.Ascending)
                        ? baseQuery.OrderBy(selectedColumn)
                        : baseQuery.OrderByDescending(selectedColumn);
                }
            }

            var orders = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
            .ToListAsync();

            return (orders, totalCount);
        }

        public async Task<Order?> GetByIdIncludeWithOrderItemsAsync(int id)
        {
            var order = await dbContext
               .Orders
               .AsNoTracking()
               .Include(o => o.Customer)
               .Include(o => o.OrderItems)
               .ThenInclude(o => o.Dish)
               .FirstOrDefaultAsync(o => o.Id == id);

            return order;
        }

    }
}
