using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Contracts;
using SportsStore.Infrastructure;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Pages {

    public class CartModel : PageModel {
        private IStoreRepository repository;

        public CartModel(IStoreRepository repo, Cart cartService) {
            repository = repo;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl) {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(long productId, string returnUrl) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
            Cart.AddLine(product, 1);
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl) {
            Cart.RemoveLine(Cart.Lines.First(cl =>
                cl.Product.ProductId == productId).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
