using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;

namespace Blossom.Pages.SellerChannel
{
    public class EditModel : PageModel
    {
        private readonly IFlowerService _flowerService;
        private readonly IUserIdAccessor _userIdAccessor;

        public EditModel(IFlowerService flowerService, IUserIdAccessor userIdAccessor)
        {
            _flowerService = flowerService;
            _userIdAccessor = userIdAccessor;
        }

        [BindProperty]
        public FlowerListing FlowerListing { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            FlowerListing = await _flowerService.GetFlowerByIdAsync(id);
            if (FlowerListing == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _flowerService.UpdateFlowerAsync(FlowerListing);

            TempData["UpdateSuccessMessage"] = "Flower listing updated successfully.";
            return RedirectToPage("/SellerChannel/Index");
        }
    }
}
