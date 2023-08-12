using System.Linq.Expressions;

namespace VehicleManagerApi.Data
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T?> GetByIdAsync(int? id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
