using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Blossom.Pages.SellerChannel
{
    public class CreateModel : PageModel
    {
        private readonly IFlowerService _flowerService;
        private readonly IUserIdAccessor _userIdAccessor;

        public CreateModel(IFlowerService flowerService, IUserIdAccessor userIdAccessor)
        {
            _flowerService = flowerService;
            _userIdAccessor = userIdAccessor;
        }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        [BindProperty]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả là bắt buộc.")]
        [BindProperty]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        [BindProperty]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc.")]
        [BindProperty]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [BindProperty]
        public string Address { get; set; }

        [Required(ErrorMessage = "Số lượng trong kho là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng trong kho phải lớn hơn 0.")]
        [BindProperty]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "URL hình ảnh là bắt buộc.")]
        [BindProperty]
        public string ImageUrl { get; set; }

        public string ErrorMessage { get; private set; }



        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string userId = _userIdAccessor.GetCurrentUserId();

            if (userId == null)
            {
                return RedirectToPage("/Auth/SignIn");
            }

            FlowerListing flower = new FlowerListing
            {
                Name = Name,
                Description = Description,
                Price = Price,
                CategoryId = CategoryId,
                StockQuantity = StockQuantity,
                ImageUrl = ImageUrl,
                SellerId = userId,
                Address = Address,
                RejectReason = "",
                Views = 0,
                IsDeleted = false

            };

            await _flowerService.AddFlowerAsync(flower);

            TempData["CreateSuccessMessage"] = "Flower listing created successfully.";

            return RedirectToPage("/SellerChannel/Index");
        }
    }
}
