using BusinessObjects.Entities;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IFlowerRepository : IGenericRepository<FlowerListing, FlowerDAO>
    {
        // Asynchronous method to get flowers by SellerId
        Task<IEnumerable<FlowerListing>> GetFlowersBySellerIdAsync(string sellerId);

        // Asynchronous method to get flowers by CategoryId
        Task<IEnumerable<FlowerListing>> GetFlowersByCategoryIdAsync(string categoryId);
    }
}
