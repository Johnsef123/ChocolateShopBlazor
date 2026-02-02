using ECommerceChocolate.Repository.Abstractions;
using ECommerceChocolate.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceChocolate.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<int> PlaceOrderAsync(OrderDetails orderDetails)
        {


            await _db.OrderDetails.AddAsync(orderDetails);
            
            if (orderDetails.orderContents.Any())
            {
                foreach (var item in orderDetails.orderContents)
                {
                    await _db.OrderContents.AddAsync(item);
                }
                Console.WriteLine("Items Passed Successfully");

            }

            await _db.SaveChangesAsync();

            return orderDetails.Id;
        }
        public async Task<bool> UpdateOrderStatusAsync(int OrderId, string newStatus )
        {
            var order = await _db.OrderDetails.Where(o => o.Id == OrderId).FirstOrDefaultAsync();
            order.Status = newStatus;
            _db.OrderDetails.Update(order);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task <IEnumerable<OrderDetails>> GetOrdersAsync(string? userId)
        {
            if(userId == null)
            {
                return await _db.OrderDetails.Include(o=>o.orderContents).ToListAsync();
            }
            return await _db.OrderDetails.Where(u=>u.UserId == userId).Include(o=>o.orderContents).ToListAsync();
        }
        public async Task<OrderDetails> GetOrderAsync(int orderId)
        {
            return await _db.OrderDetails.Where(u=>u.Id == orderId).Include(o=>o.orderContents).FirstOrDefaultAsync();
        }
    }
}
