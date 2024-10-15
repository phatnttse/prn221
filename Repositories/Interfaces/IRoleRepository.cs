using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(string roleId);
        Task<Role?> GetByNameAsync(string roleName);
        Task<Role?> GetLoadRelatedAsync(string roleName);
        Task<List<Role>> GetAllLoadRelatedAsync(int page, int pageSize);
        Task CreateAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(Role role);
    }
}
