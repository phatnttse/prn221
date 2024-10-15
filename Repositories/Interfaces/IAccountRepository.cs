using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetUserByIdAsync(string userId);
        Task<Account?> GetUserByEmailAsync(string email);
        Task<(Account User, string[] Roles)?> GetUserAndRolesAsync(string userId);
        Task<List<(Account User, string[] Roles)>> GetUsersAndRolesAsync(int page, int pageSize);
    }
}
