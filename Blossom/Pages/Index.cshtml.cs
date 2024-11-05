using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace Blossom.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<FlowerListing> FlowerList { get; set; }
        [BindProperty]
        public List<Category> CategoryList { get; set; }

        private readonly IFlowerService _flowerService;
        //private readonly ICategoryService _categoryService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IFlowerService flowerService)
        {
            _logger = logger;
            _flowerService = flowerService;
            FlowerList = new();
            CategoryList = new();
        }

        public async Task<IActionResult> OnGet()
        {
            FlowerList = (await _flowerService.GetAllFlowersAsync()).ToList();
            return Page();
        }
    }
}
