using BusinessObjects.Entities;
using DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace DAO
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
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

        public async Task<(Account User, string[] Roles)?> GetUserAndRolesAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            var roleIds = user.Roles.Select(r => r.RoleId).ToList();
            var roles = await _context.Roles
                .Where(r => roleIds.Contains(r.Id))
                .Select(r => r.Name!)
                .ToArrayAsync();

            return (user, roles);
        }

        public async Task<List<(Account User, string[] Roles)>> GetUsersAndRolesAsync(int page, int pageSize)
        {
            IQueryable<Account> usersQuery = _context.Users.Include(u => u.Roles);
            var totalUsers = await usersQuery.CountAsync();
            var users = await usersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new List<(Account, string[])>();

            foreach (var user in users)
            {
                var roleIds = user.Roles.Select(r => r.RoleId).ToList();
                var roles = await _context.Roles
                    .Where(r => roleIds.Contains(r.Id))
                    .Select(r => r.Name!)
                    .ToArrayAsync();

                result.Add((user, roles));
            }

            return result;
        }
    }
}
