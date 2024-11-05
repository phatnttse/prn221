using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CategoryDAO : GenericDAO<Category>
    {
        public CategoryDAO(ApplicationDbContext context) : base(context)
        {
        }

        // Get category by its Id asynchronously
        public async Task<Category?> GetCategoryByIdAsync(string categoryId)
        {
            return await _context.Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();
        }

        // Get category by its Name asynchronously
        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories
                .Where(c => c.Name == categoryName)
                .FirstOrDefaultAsync();
        }

        // Get all categories with pagination support
        public async Task<List<Category>> GetAllCategoriesAsync(int page, int pageSize)
        {
            IQueryable<Category> categoriesQuery = _context.Categories;

            if (page > 0)
                categoriesQuery = categoriesQuery.Skip((page - 1) * pageSize);

            if (pageSize > 0)
                categoriesQuery = categoriesQuery.Take(pageSize);

            return await categoriesQuery.ToListAsync();
        }

        // Create a new category asynchronously
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // Update an existing category asynchronously
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            Category existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.ImageUrl = category.ImageUrl;
            }
            _context.Categories.Update(existingCategory);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        // Delete a category by its Id asynchronously
        public async Task<bool> DeleteCategoryAsync(string categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
            if (category == null) return false;

            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
