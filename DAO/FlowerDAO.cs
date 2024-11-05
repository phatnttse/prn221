using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class FlowerDAO : GenericDAO<FlowerListing>
    {
        public FlowerDAO(ApplicationDbContext context): base(context)
        {
        }
        // Get flowers by SellerId asynchronously
        public async Task<IEnumerable<FlowerListing>> GetFlowersBySellerIdAsync(string sellerId)
        {
            return await _context.FlowerListings
                .Include(f => f.Seller)
                .Where(f => f.SellerId == sellerId && !f.IsDeleted)
                .ToListAsync();
        }

        // Get flowers by CategoryId asynchronously
        public async Task<IEnumerable<FlowerListing>> GetFlowersByCategoryIdAsync(string categoryId)
        {
            return await _context.FlowerListings
                .Include(f => f.Category)
                .Where(f => f.CategoryId == categoryId && !f.IsDeleted)
                .ToListAsync();
        }
    }
}
