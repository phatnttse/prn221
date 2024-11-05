using BusinessObjects.Entities;
using Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Repositories.Interfaces;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<Account> _userManager;

        public AccountService(IAccountRepository accountRepository, UserManager<Account> userManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        public async Task<Account?> GetUserByIdAsync(string userId)
        {
            return await _accountRepository.GetUserByIdAsync(userId);
        }

        public async Task<Account?> GetUserByEmailAsync(string email)
        {
            return await _accountRepository.GetUserByEmailAsync(email);
        }

        public async Task<IList<string>> GetUserRolesAsync(Account user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<(Account User, string[] Roles)?> GetUserAndRolesAsync(string userId)
        {
            return await _accountRepository.GetUserAndRolesAsync(userId);
        }

        public async Task<List<(Account User, string[] Roles)>> GetUsersAndRolesAsync(int page, int pageSize)
        {
            return await _accountRepository.GetUsersAndRolesAsync(page, pageSize);
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateUserAsync(Account user, IEnumerable<string> roles, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            user = (await _userManager.FindByNameAsync(user.UserName!))!;

            try
            {
                result = await _userManager.AddToRolesAsync(user, roles.Distinct());
            }
            catch
            {
                await DeleteUserAsync(user);
                throw;
            }

            if (!result.Succeeded)
            {
                await DeleteUserAsync(user);
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }

            return (true, Array.Empty<string>());
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(Account user)
        {
            return await UpdateUserAsync(user, null);
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(Account user, IEnumerable<string>? roles)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            if (roles != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var rolesToRemove = userRoles.Except(roles).ToArray();
                var rolesToAdd = roles.Except(userRoles).Distinct().ToArray();

                if (rolesToRemove.Length != 0)
                {
                    result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description).ToArray());
                }

                if (rolesToAdd.Length != 0)
                {
                    result = await _userManager.AddToRolesAsync(user, rolesToAdd);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description).ToArray());
                }
            }

            return (true, Array.Empty<string>());
        }

        public async Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(Account user, string newPassword)
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdatePasswordAsync(Account user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, Array.Empty<string>());
        }

        public async Task<bool> CheckPasswordAsync(Account user, string password)
        {
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                if (!_userManager.SupportsUserLockout)
                    await _userManager.AccessFailedAsync(user);

                return false;
            }

            return true;
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
                return await DeleteUserAsync(user);

            return (true, Array.Empty<string>());
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(Account user)
        {
            var result = await _userManager.DeleteAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }
    }
}
