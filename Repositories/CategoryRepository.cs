using BusinessObjects.Entities;
using DAO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _dao;

        // Constructor to inject the CategoryDAO into the repository
        public CategoryRepository(CategoryDAO dao)
        {
            _dao = dao;
        }

        // Create a new category
        public async Task<Category?> CreateCategoryAsync(Category category)
        {
            return await _dao.CreateCategoryAsync(category);
        }

        // Delete a category by its Id
        public async Task<bool> DeleteCategoryAsync(string categoryId)
        {
            return await _dao.DeleteCategoryAsync(categoryId);
        }

        // Get all categories with pagination support
        public async Task<List<Category>> GetAllCategoriesAsync(int page, int pageSize)
        {
            return await _dao.GetAllCategoriesAsync(page, pageSize);
        }

        // Get a category by its Id
        public async Task<Category?> GetCategoryByIdAsync(string categoryId)
        {
            return await _dao.GetCategoryByIdAsync(categoryId);
        }

        // Get a category by its Name
        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
            return await _dao.GetCategoryByNameAsync(categoryName);
        }

        // Update an existing category
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            return await _dao.UpdateCategoryAsync(category);
        }
    }
}
