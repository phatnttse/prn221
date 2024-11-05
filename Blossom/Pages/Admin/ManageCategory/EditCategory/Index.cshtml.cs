using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace Blossom.Pages.Admin.ManageCategory.EditCategory
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; }

        [BindProperty]
        public string CategoryId { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();  
            }

            Category = await _categoryService.GetCategoryByIdAsync(id);
 

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Category.Id = CategoryId;
                await _categoryService.UpdateCategoryAsync(Category);
                ModelState.AddModelError(string.Empty, "There was an error updating the category.");
            }
            return Page();
        }
    }
}
