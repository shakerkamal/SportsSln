using Microsoft.AspNetCore.Mvc;
using SportsStore.Contracts;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository storeRepository;
        public int PageSize = 5;

        public HomeController(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public ViewResult Index(string category, int productPage = 1)
        {
            return View(new ProductListViewModel
            {
                Products = storeRepository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(x => x.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        storeRepository.Products.Count() :
                        storeRepository.Products
                        .Where(x => x.Category == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}
