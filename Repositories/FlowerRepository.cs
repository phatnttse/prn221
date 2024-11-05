using BusinessObjects.Entities;
using DAO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FlowerRepository : GenericRepository<FlowerListing, FlowerDAO>, IFlowerRepository
    {
        public FlowerRepository(FlowerDAO dao) : base(dao)
        {
        }

        public async Task<IEnumerable<FlowerListing>> GetFlowersByCategoryIdAsync(string categoryId)
        {
            return await base._dao.GetFlowersByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<FlowerListing>> GetFlowersBySellerIdAsync(string sellerId)
        {
            return await base._dao.GetFlowersBySellerIdAsync(sellerId);
        }
    }
}
