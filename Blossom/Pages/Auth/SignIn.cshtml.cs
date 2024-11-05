using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;

namespace Blossom.Pages.Auth
{
    public class SignInModel : PageModel
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; private set; }

        public SignInModel(SignInManager<Account> signInManager, UserManager<Account> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Email and password cannot be empty.";
                return Page();
            }

            Account user = await _userManager.FindByNameAsync(Email) ?? await _userManager.FindByEmailAsync(Email);
            if (user == null) 
            {
                ErrorMessage = "Invalid login attempt or account is disabled.";
                return Page();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, Password, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Index"); 
            }
            else
            {
                ErrorMessage = result.IsLockedOut ? "Account is locked." : "Invalid login attempt.";
                return Page();
            }
        }
    }
}
