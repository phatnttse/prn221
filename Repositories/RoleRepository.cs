using BusinessObjects.Entities;
using DAO;
using Repositories.Interfaces;


namespace Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleDAO _roleDAO;

        public RoleRepository(RoleDAO roleDAO)
        {
            _roleDAO = roleDAO;
        }

        public Task<Role?> GetByIdAsync(string roleId)
        {
            return _roleDAO.GetByIdAsync(roleId);
        }

        public Task<Role?> GetByNameAsync(string roleName)
        {
            return _roleDAO.GetByNameAsync(roleName);
        }

        public Task<Role?> GetLoadRelatedAsync(string roleName)
        {
            return _roleDAO.GetLoadRelatedAsync(roleName);
        }

        public Task<List<Role>> GetAllLoadRelatedAsync(int page, int pageSize)
        {
            return _roleDAO.GetAllLoadRelatedAsync(page, pageSize);
        }

        public Task CreateAsync(Role role)
        {
            return _roleDAO.CreateAsync(role);
        }

        public Task UpdateAsync(Role role)
        {
            return _roleDAO.UpdateAsync(role);
        }

        public Task DeleteAsync(Role role)
        {
            return _roleDAO.DeleteAsync(role);
        }
    }
}
