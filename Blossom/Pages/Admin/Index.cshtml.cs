using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using BusinessObjects;

namespace Blossom.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;


        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task OnGetAsync()
        {
            Categories = await _categoryService.GetAllCategoriesAsync(1, 10);
        }


        public async Task<IActionResult> OnPostDeleteCategoryAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            await _categoryService.RemoveCategoryAsync(id);

            Categories = await _categoryService.GetAllCategoriesAsync(1, 10);
          

            return RedirectToPage();
        }

    }
}
