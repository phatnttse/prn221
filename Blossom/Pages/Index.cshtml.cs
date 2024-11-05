using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blossom.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<FlowerListing> FlowerList { get; set; }
        [BindProperty]
        public List<Category> CategoryList { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            FlowerList = new();
            CategoryList = new();
        }

        public void OnGet()
        {

        }
    }
}
