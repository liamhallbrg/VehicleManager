using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace VehicleManager.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
