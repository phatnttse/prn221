using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFlowerService
    {
        Task<IEnumerable<FlowerListing>> GetAllFlowersAsync();
        Task<FlowerListing> GetFlowerByIdAsync(string id);
        Task<IEnumerable<FlowerListing>> GetFlowersByCategoryIdAsync(string categoryId);
        Task<IEnumerable<FlowerListing>> GetFlowersBySellerIdAsync(string sellerId);
        Task AddFlowerAsync(FlowerListing flower);
        Task UpdateFlowerAsync(FlowerListing flower);
        Task RemoveFlowerAsync(FlowerListing flower);
    }
}
