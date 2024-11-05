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
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerRepository _flowerRepository;

        public FlowerService(IFlowerRepository flowerRepository)
        {
            _flowerRepository = flowerRepository;
        }

        // Get all flowers
        public async Task<IEnumerable<FlowerListing>> GetAllFlowersAsync()
        {
            return await _flowerRepository.GetAllAsync();
        }

        // Get a flower by ID
        public async Task<FlowerListing> GetFlowerByIdAsync(string id)
        {
            return await _flowerRepository.GetByIdAsync(id);
        }

        // Get flowers by category ID
        public async Task<IEnumerable<FlowerListing>> GetFlowersByCategoryIdAsync(string categoryId)
        {
            return await _flowerRepository.GetFlowersByCategoryIdAsync(categoryId);
        }

        // Get flowers by seller ID
        public async Task<IEnumerable<FlowerListing>> GetFlowersBySellerIdAsync(string sellerId)
        {
            return await _flowerRepository.GetFlowersBySellerIdAsync(sellerId);
        }

        // Add a new flower listing
        public async Task AddFlowerAsync(FlowerListing flower)
        {
            await _flowerRepository.AddAsync(flower);
        }

        // Update an existing flower listing
        public async Task UpdateFlowerAsync(FlowerListing flower)
        {
            await _flowerRepository.UpdateAsync(flower);
        }

        // Remove a flower listing
        public async Task RemoveFlowerAsync(FlowerListing flower)
        {
            await _flowerRepository.RemoveAsync(flower);
        }
    }
}
