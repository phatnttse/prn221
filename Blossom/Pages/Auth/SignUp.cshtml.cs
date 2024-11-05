using BusinessObjects.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace Blossom.Pages.Auth
{
    public class SignUpModel : PageModel
    {
        private readonly IAuthService _authService;

        public SignUpModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public Gender? Gender { get; set; }
        public string ErrorMessage { get; private set; }

        public IActionResult OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index"); // Replace with the path to your home page
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            // Check if Gender has a value before parsing
            if (Gender == null)
            {
                ErrorMessage = "Gender is required.";
                return Page();
            }

            var response = await _authService.RegisterAsync(Email, Password, Gender.Value);

            if (response.Success)
            {
                TempData["SignUpSuccessMessage"] = response.Message;
                return RedirectToPage("/Auth/SignIn");
            }
            else
            {
                ErrorMessage = response.Message;
                return Page();
            }
        }


    }
}
