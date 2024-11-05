using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace DAO
{
    public class AccountRepository :  IAccountRepository
    {
        private readonly  AccountDAO _dao;

        public AccountRepository(AccountDAO dao)
        {
            _dao = dao;
        }

        public async Task<Account?> GetUserByIdAsync(string userId)
        {
            return await _dao.GetUserByIdAsync(userId);
        }

        public async Task<Account?> GetUserByEmailAsync(string email)
        {
            return await _dao.GetUserByEmailAsync(email);
        }

        public async Task<(Account User, string[] Roles)?> GetUserAndRolesAsync(string userId)
        {
            return await _dao.GetUserAndRolesAsync(userId);
        }

        public async Task<List<(Account User, string[] Roles)>> GetUsersAndRolesAsync(int page, int pageSize)
        {
            return await _dao.GetUsersAndRolesAsync(page, pageSize);
        }
    }
}
