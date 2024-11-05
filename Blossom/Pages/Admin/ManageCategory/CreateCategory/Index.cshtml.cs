using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace Blossom.Pages.Admin.ManageCategory.CreateCategory
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category NewCategory { get; set; }

        public void OnGet()
        {
            NewCategory = new Category();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(NewCategory);
                return RedirectToPage("/Admin/ManageCategory/Index");
            }
            return Page();
        }
    }
}
