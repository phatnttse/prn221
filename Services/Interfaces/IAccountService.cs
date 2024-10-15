using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CheckPasswordAsync(Account user, string password);
        Task<(bool Succeeded, string[] Errors)> CreateUserAsync(Account user, IEnumerable<string> roles, string password);
        Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(Account user);
        Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string userId);
        Task<(Account User, string[] Roles)?> GetUserAndRolesAsync(string userId);
        Task<Account?> GetUserByEmailAsync(string email);
        Task<Account?> GetUserByIdAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(Account user);
        Task<List<(Account User, string[] Roles)>> GetUsersAndRolesAsync(int page, int pageSize);
        Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(Account user, string newPassword);
        Task<(bool Succeeded, string[] Errors)> UpdatePasswordAsync(Account user, string currentPassword, string newPassword);
        Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(Account user);
        Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(Account user, IEnumerable<string>? roles);
    }
}
