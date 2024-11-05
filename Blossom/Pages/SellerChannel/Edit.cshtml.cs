using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;
using BusinessObjects.Entities.Enums;

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
        public string FlowerId { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public decimal Price { get; set; }

        [BindProperty]
        public int StockQuantity { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string ImageUrl { get; set; }

        [BindProperty]
        public string CategoryId { get; set; }

        public FlowerListingStatus Status { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var flowerListing = await _flowerService.GetFlowerByIdAsync(id);
            if (flowerListing == null)
            {
                return NotFound();
            }

            FlowerId = flowerListing.Id;
            Name = flowerListing.Name;
            Description = flowerListing.Description;
            Price = flowerListing.Price;
            StockQuantity = flowerListing.StockQuantity;
            Address = flowerListing.Address;
            ImageUrl = flowerListing.ImageUrl;
            CategoryId = flowerListing.CategoryId;
            Status = flowerListing.Status;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Kiểm tra tính hợp lệ
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string userId = _userIdAccessor.GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToPage("/Auth/SignIn");
            }

            // Tạo đối tượng FlowerListing mới từ các thuộc tính riêng
            var flowerListing = new FlowerListing
            {
                Id = FlowerId,
                Name = Name,
                Description = Description,
                Price = Price,
                StockQuantity = StockQuantity,
                Address = Address,
                ImageUrl = ImageUrl,
                CategoryId = CategoryId,
                Status = Status,
                SellerId = userId,
                RejectReason = "",
            };

            await _flowerService.UpdateFlowerAsync(flowerListing);

            TempData["UpdateSuccessMessage"] = "Flower listing updated successfully.";
            return RedirectToPage("/SellerChannel/Index");
        }
    }
}
