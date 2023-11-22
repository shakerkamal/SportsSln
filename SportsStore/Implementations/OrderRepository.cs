using Microsoft.EntityFrameworkCore;
using SportsStore.Contracts;
using SportsStore.Models;

namespace SportsStore.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _dbContext;

        public OrderRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Order> Orders => _dbContext.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
            {
                _dbContext.Orders.Add(order);
            }
            _dbContext.SaveChanges();
        }
    }
}
