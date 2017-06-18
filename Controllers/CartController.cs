using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
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
        private Cart GetCart()
        {

            Cart cart = HttpContext.Session.GetJson<Cart>("Cart");
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
        
        public RedirectToActionResult AddToCart(int productID,string returnurl)
        {
            Product product = repository.Products.Where(x => x.ProductId == productID).FirstOrDefault();
            if(product == null)
            {
                Cart cart = GetCart();
                cart.AddItem()
               
            }
        }
    }
}
