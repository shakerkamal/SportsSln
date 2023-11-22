using SportsStore.Models;

namespace SportsStore.Contracts
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
