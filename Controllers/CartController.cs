using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Infrastructure;
using WebApplication7.Models;
using WebApplication7.Models.ViewModels;

namespace WebApplication7.Controllers
{
    public class CartController : Controller
    {
        private Cart cart;
        private readonly IProductRepository repository;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }


        public RedirectToActionResult AddToCart(int productID, string returnurl)
        {
            var product = repository.Products.Where(x => x.ProductId == productID).FirstOrDefault();
            if (product != null)
            {
                var cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new {returnurl});
        }

        public RedirectToActionResult RemoveFromCart(int productID, string returnurl)
        {
            var product = repository.Products.Where(x => x.ProductId == productID).FirstOrDefault();
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new {returnurl});
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
                {
                    Cart = GetCart(),
                    returnUrl = returnUrl
                }
            );
        }
    }
}