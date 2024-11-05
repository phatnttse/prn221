using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        // Get all categories with pagination support
        Task<List<Category>> GetAllCategoriesAsync(int page, int pageSize);

        // Get a category by its ID
        Task<Category?> GetCategoryByIdAsync(string categoryId);

        // Get a category by its Name
        Task<Category?> GetCategoryByNameAsync(string categoryName);

        // Add a new category
        Task AddCategoryAsync(Category category);

        // Update an existing category
        Task UpdateCategoryAsync(Category category);

        // Remove a category by its ID
        Task RemoveCategoryAsync(string categoryId);
    }
}
