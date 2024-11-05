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
    public class CartItemRepository : GenericRepository<CartItem, CartItemDAO>, ICartItemRepository
    {
        public CartItemRepository(CartItemDAO dao) : base(dao)
        {
        }
    }
}
