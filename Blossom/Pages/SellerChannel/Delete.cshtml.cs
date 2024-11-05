using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using BusinessObjects.Entities;

namespace Blossom.Pages.SellerChannel
{
    public class DeleteModel : PageModel
    {
        private readonly IFlowerService _flowerService;

        public DeleteModel(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [BindProperty]
        public FlowerListing FlowerListing { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            FlowerListing = await _flowerService.GetFlowerByIdAsync(id); // L?y thông tin FlowerListing theo ID

            if (FlowerListing == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (FlowerListing != null)
            {
                await _flowerService.RemoveFlowerAsync(FlowerListing); // G?i d?ch v? xóa
                TempData["DeleteSuccessMessage"] = "Flower listing deleted successfully.";
                return RedirectToPage("/SellerChannel/Index");
            }

            return Page();
        }
    }
}
