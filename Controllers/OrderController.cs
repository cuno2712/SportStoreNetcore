using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using Microsoft.AspNetCore.Authorization;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Mailgun;

namespace WebApplication7.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Order());
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Empty cart");
            }
            if(ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                decimal total = 0;
                foreach(var item in order.Lines)
                {
                    total = total+ (item.Quantity*item.Product.Price);
                }
                Email.DefaultRenderer = new RazorRenderer();
                var sender = new MailgunSender(
    "sandbox5794cb5050f84b339bbf1e38e4a73202.mailgun.org", // Mailgun Domain
    "key-21e40c5f3b6846415e516edbcbee371a" // Mailgun API Key
);
                Email.DefaultSender = sender;
                var template = "Dear @Model.Name, Your order total @Model.Total.";
                var email = Email
    .From("danangofme@gmail.com")
    .To(order.Email)
    .Subject("Order")
    .UsingTemplate(template, new { Name = order.Name, Total = total.ToString() });
                var response = await email.SendAsync();
                return RedirectToAction(nameof(Completed));
            }
            else
                return View(order);
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
        [Authorize]
        public ViewResult List() => View(repository.Orders.Where(x => !x.Shipped));
        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders.FirstOrDefault(x=>x.OrderID==orderID);
            if(order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));

        }
    }
}