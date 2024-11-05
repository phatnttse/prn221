using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace Blossom.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly IAuthService _authService;
        public LogoutModel(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<IActionResult> OnGet()
        {
            await _authService.LogoutAsync();
            return RedirectToPage("/Index");
        }
    }
}
