using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public Task<Order> GetOrderByIdAsync(string id);
        public Task AddOrderAsync(Order order);
        public Task UpdateOrderAsync(Order order);
        public Task RemoveOrderAsync(Order order);
    }
}
