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

        public void CreateProduct(Product p)
        {
            _dbContext.Add(p);
            _dbContext.SaveChanges();
        }
        public void DeleteProduct(Product p)
        {
            _dbContext.Remove(p);
            _dbContext.SaveChanges();
        }
        public void SaveProduct(Product p)
        {
            _dbContext.SaveChanges();
        }
    }
}
