using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories.GenericRepository;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    public class RatingsRepository(RestaurantsDbContext dbContext) : GenericRepository<Rating>(dbContext), IRatingsRepository
    {
        public async Task<(IEnumerable<Rating> Ratings, int TotalCount)> GetAllMatchingAsync(
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
        {
            string? searchPhraseLower = searchPhrase?.Trim().ToLower();

            // جرّب تحويل البحث إلى رقم أو تاريخ إن أمكن
            bool isInt = int.TryParse(searchPhraseLower, out int star);
            bool isDate = DateTime.TryParse(searchPhrase, out DateTime date);

            IQueryable<Rating> baseQuery = dbContext.Ratings
                .AsNoTracking()
                .Include(r => r.Restaurant)
                .Include(r => r.Dish)
                .Include(r => r.Customer)
                .Where(r =>
                    searchPhraseLower == null ||
                    r.Comment!.ToLower().Contains(searchPhraseLower) ||
                    (isInt && r.Stars == star) ||
                    (isDate && r.CreatedAt.Date == date.Date));

            int totalCount = await baseQuery.CountAsync();

            // إعداد أعمدة الترتيب المسموح بها
            var columnsSelector = new Dictionary<string, Expression<Func<Rating, object>>>
    {
        { nameof(Rating.Comment),   r => r.Comment!   },
        { nameof(Rating.Stars),     r => r.Stars      },
        { nameof(Rating.CreatedAt), r => r.CreatedAt  },
    };

            if (!string.IsNullOrEmpty(sortBy) && columnsSelector.TryGetValue(sortBy, out var selectedColumn))
            {
                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var ratings = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (ratings, totalCount);
        }

        public async Task<Rating?> GetByIdWithIncluded(int id)
        {
            var rating = await dbContext.Ratings
                .AsNoTracking()
                .Include(r => r.Restaurant)
                .Include(r => r.Dish)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);

            return rating;
        }
    }
}
