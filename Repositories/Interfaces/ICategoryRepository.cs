using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryByIdAsync(string categoryId);
        Task<Category?> GetCategoryByNameAsync(string categoryName);
        Task<List<Category>> GetAllCategoriesAsync(int page, int pageSize);
        Task<Category?> CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(string categoryId);
    }
}
