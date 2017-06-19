using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using WebApplication7.Infrastructure;


namespace WebApplication7.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
       
        
        public RedirectToActionResult AddToCart(int productID,string returnurl)
        {
            Product product = repository.Products.Where(x => x.ProductId == productID).FirstOrDefault();
            if(product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
               
            }
            return RedirectToAction("Index", new { returnurl });
        }
        public RedirectToActionResult RemoveFromCart(int productID,string returnurl)
        {
            Product product = repository.Products.Where(x => x.ProductId == productID).FirstOrDefault();
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnurl });


        }
        private Cart GetCart()
        {

            Cart cart = HttpContext.Session.GetJson<Cart>("Cart")??new Cart();
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
