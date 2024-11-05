using DAO;
using System.Linq.Expressions;

namespace Repositories.Interfaces
{
    public interface IGenericRepository<T, V> where T : class where V : GenericDAO<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveChangesAsync();
    }
}
