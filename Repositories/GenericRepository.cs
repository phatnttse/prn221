using DAO;
using System.Linq.Expressions;

namespace Repositories
{
    public class GenericRepository<T> : GenericDAO<T> where T : class
    {
        private readonly GenericDAO<T> _dao;

        public GenericRepository(ApplicationDbContext context) : base(context)
        {
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
