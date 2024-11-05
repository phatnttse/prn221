using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CartItemDAO : GenericDAO<CartItem>
    {
        public CartItemDAO(ApplicationDbContext context) : base(context)
        {
        }

        // Get all cart items for a specific user
        public async Task<IEnumerable<CartItem>> GetAllByUserAsync(Account user)
        {
            return await _context.CartItems.Where(ci => ci.UserId == user.Id).ToListAsync();
        }

        // Find a cart item by user and flower listing
        public async Task<CartItem> GetByUserAndFlowerAsync(Account user, FlowerListing flowerListing)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == user.Id && ci.FlowerListingId == flowerListing.Id);
        }

        // Delete all cart items for a specific user
        public async Task DeleteAllByUserAsync(Account user)
        {
            var items = await GetAllByUserAsync(user);
            _context.CartItems.RemoveRange(items);
            await SaveChangesAsync();
        }

        // Count cart items for a specific flower user within a date range
        public async Task<int> CountCartItemsByFlowerUserAndDateRangeAsync(Account seller, DateTime startDate, DateTime endDate)
        {
            return await _context.CartItems
                .CountAsync(ci => ci.FlowerListing.SellerId == seller.Id &&
                                  ci.CreatedAt >= startDate &&
                                  ci.CreatedAt <= endDate);
        }
    }
}
