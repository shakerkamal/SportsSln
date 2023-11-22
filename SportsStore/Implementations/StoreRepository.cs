using SportsStore.Contracts;
using SportsStore.Models;

namespace SportsStore.Implementations
{
    public class StoreRepository : IStoreRepository
    {
        private StoreDbContext _dbContext;

        public StoreRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> Products => _dbContext.Products;
    }
}
