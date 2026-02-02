using ECommerceChocolate.Data;
using Microsoft.AspNetCore.Identity;

namespace ECommerceChocolate.Repository.Abstractions
{
    public interface IOrderRepository
    {
        public Task <int> PlaceOrderAsync(OrderDetails orderDetails);
        public Task<bool> UpdateOrderStatusAsync(int OrderId,string status);
        public Task<IEnumerable<OrderDetails>> GetOrdersAsync(string? userId);
        public Task<OrderDetails> GetOrderAsync(int orderId);
        

    }
}
