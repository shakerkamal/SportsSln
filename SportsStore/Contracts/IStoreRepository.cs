using SportsStore.Models;

namespace SportsStore.Contracts
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}
