using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace Blossom.Pages.Auth
{
    public class SignInModel : PageModel
    {
        private readonly IAuthService _authService;

        public SignInModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; private set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid data.";
                return Page();
            }

            var response = await _authService.SignInAsync(Email, Password);

            if (response.Success)
            {
                TempData["LoginSuccessMessage"] = response.Message;
                return RedirectToPage("/Index");
            }
            else
            {
                // Display error message from response
                ErrorMessage = response.Message;
                return Page();
            }
        }
    }
}
