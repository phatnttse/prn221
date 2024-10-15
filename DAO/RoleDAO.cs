using BusinessObjects.Entities;
using DAO.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAO
{
    public class RoleDAO 
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Role> _roleManager;

        public RoleDAO(ApplicationDbContext context, RoleManager<Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<Role?> GetByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<Role?> GetLoadRelatedAsync(string roleName)
        {
            return await _context.Roles
                .Include(r => r.Claims)
                .Include(r => r.Users)
                .SingleOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<List<Role>> GetAllLoadRelatedAsync(int page, int pageSize)
        {
            IQueryable<Role> rolesQuery = _context.Roles
                .Include(r => r.Claims)
                .Include(r => r.Users)
                .OrderBy(r => r.Name);

            if (page > 0)
                rolesQuery = rolesQuery.Skip((page - 1) * pageSize);

            if (pageSize > 0)
                rolesQuery = rolesQuery.Take(pageSize);

            return await rolesQuery.ToListAsync();
        }

        public async Task CreateAsync(Role role)
        {
            await _roleManager.CreateAsync(role);
        }

        public async Task UpdateAsync(Role role)
        {
            await _roleManager.UpdateAsync(role);
        }

        public async Task DeleteAsync(Role role)
        {
            await _roleManager.DeleteAsync(role);
        }
    }
}
