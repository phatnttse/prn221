using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }


        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        public async Task RemoveOrderAsync(Order order)
        {
            await _orderRepository.RemoveAsync(order);
        }
    }
}
