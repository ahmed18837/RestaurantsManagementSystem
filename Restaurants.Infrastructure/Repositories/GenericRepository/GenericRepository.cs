using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Repositories.GenericRepository;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories.GenericRepository
{
    public class GenericRepository<T>(RestaurantsDbContext dbContext) : IGenericRepository<T> where T : class
    {
        private DbSet<T> Set => dbContext.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync()
            => await Set.ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await Set.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await Set.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Set.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
