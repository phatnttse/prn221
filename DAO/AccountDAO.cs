using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;

namespace DAO
{
    public class AccountDao
    {
        private readonly ApplicationDbContext _context;

        public AccountDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<Account?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IList<string>> GetUserRolesAsync(Account user)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId)
                .ToListAsync();
        }

        public async Task<(Account User, string[] Roles)?> GetUserAndRolesAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) return null;

            var roleIds = await GetUserRolesAsync(user);
            var roles = await _context.Roles
                .Where(r => roleIds.Contains(r.Id))
                .Select(r => r.Name!)
                .ToArrayAsync();

            return (user, roles);
        }

        public async Task<List<(Account User, string[] Roles)>> GetUsersAndRolesAsync(int page, int pageSize)
        {
            IQueryable<Account> usersQuery = _context.Users;

            if (page > 0)
                usersQuery = usersQuery.Skip((page - 1) * pageSize);
            if (pageSize > 0)
                usersQuery = usersQuery.Take(pageSize);

            var users = await usersQuery.ToListAsync();
            var userRoles = await _context.UserRoles
                .Where(ur => users.Select(u => u.Id).Contains(ur.UserId))
                .ToListAsync();

            return users.Select(u => (u, userRoles
                .Where(ur => ur.UserId == u.Id)
                .Select(ur => ur.RoleId)
                .ToArray()))
                .ToList();
        }
    }
}
