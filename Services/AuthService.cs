using Microsoft.AspNetCore.Identity;
using BusinessObjects.Models;
using BusinessObjects.Entities;
using Services.Interfaces;
using BusinessObjects.Entities.Enums;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthService(SignInManager<Account> signInManager, UserManager<Account> userManager, RoleManager<Role> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AppResponse<bool>> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AppResponse<bool>(false, "User not found", false, "404");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new AppResponse<bool>(true, "Login successful", true);
            }
            else if (result.IsLockedOut)
            {
                return new AppResponse<bool>(false, "Account is locked", false, "403");
            }
            else
            {
                return new AppResponse<bool>(false, "Invalid login attempt", false, "401");
            }
        }

        public async Task<AppResponse<bool>> RegisterAsync(string email, string password, Gender gender)
        {
            var user = new Account
            {
                Email = email,
                Gender = gender,
                UserName = email,
                Avatar = "~/assets/images/avatar.svg",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var defaultRole = RoleName.USER.ToString();

                if (!await _roleManager.RoleExistsAsync(defaultRole))
                {
                    await _roleManager.CreateAsync(new Role { Name = defaultRole });
                }

                await _userManager.AddToRoleAsync(user, defaultRole);

                return new AppResponse<bool>(true, "Registration successful", true);
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new AppResponse<bool>(false, "Registration failed: " + errors, false, "400");
        }


        public async Task<AppResponse<bool>> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return new AppResponse<bool>(true, "Logout successful", true);
        }
    }
}
