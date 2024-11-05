using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using BusinessObjects.Entities;


namespace Blossom.Pages.SellerChannel
{
    public class IndexModel : PageModel
    {
        private readonly IFlowerService _flowerService;
        private readonly IUserIdAccessor _userIdAccessor;

        public IndexModel(IFlowerService flowerService, IUserIdAccessor userIdAccessor)
        {
            _flowerService = flowerService;
            _userIdAccessor = userIdAccessor;
        }

        public IEnumerable<FlowerListing> FlowerListings { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _userIdAccessor.GetCurrentUserId();

            if (userId == null)
            {
                return RedirectToPage("/Auth/SignIn");
            }

            FlowerListings = await _flowerService.GetFlowersBySellerIdAsync(userId);

            return Page();
        }
    }
}
