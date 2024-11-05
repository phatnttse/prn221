using DAO;
using System.Linq.Expressions;

namespace Repositories
{
    public class GenericRepository<T, V> where T : class where V : GenericDAO<T>
    {
        protected readonly V _dao;

        public GenericRepository(V dao)
        {
            _dao = dao;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dao.FindAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await _dao.AddAsync(entity);
            await SaveChangesAsync();  
        }

        public async Task UpdateAsync(T entity)
        {
            await _dao.UpdateAsync(entity);
            await SaveChangesAsync();  
        }

        public async Task RemoveAsync(T entity)
        {
            await _dao.RemoveAsync(entity);
            await SaveChangesAsync();  
        }

        public async Task SaveChangesAsync()
        {
            await _dao.SaveChangesAsync();  
        }
    }

}
