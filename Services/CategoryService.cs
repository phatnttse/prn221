using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        // Constructor to inject the ICategoryRepository dependency
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Get all categories with pagination
        public async Task<List<Category>> GetAllCategoriesAsync(int page, int pageSize)
        {
            return await _categoryRepository.GetAllCategoriesAsync(page, pageSize);
        }

        // Get a category by its ID
        public async Task<Category?> GetCategoryByIdAsync(string categoryId)
        {
            return await _categoryRepository.GetCategoryByIdAsync(categoryId);
        }

        // Get a category by its Name
        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
            return await _categoryRepository.GetCategoryByNameAsync(categoryName);
        }

        // Add a new category
        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.CreateCategoryAsync(category);
        }

        // Update an existing category
        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        // Remove a category by its ID
        public async Task RemoveCategoryAsync(string categoryId)
        {
            await _categoryRepository.DeleteCategoryAsync(categoryId);
        }
    }
}

