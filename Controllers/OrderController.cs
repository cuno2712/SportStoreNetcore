using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Mailgun;
using FluentEmail.Razor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class OrderController : Controller
    {
        private readonly Cart cart;
        private readonly IOrderRepository repository;

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
                ModelState.AddModelError("", "Empty cart");
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                decimal total = 0;
                foreach (var item in order.Lines)
                    total = total + item.Quantity * item.Product.Price;
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
                    .UsingTemplate(template, new {order.Name, Total = total.ToString()});
                var response = await email.SendAsync();
                return RedirectToAction(nameof(Completed));
            }
            return View(order);
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }

        [Authorize]
        public ViewResult List()
        {
            return View(repository.Orders.Where(x => !x.Shipped));
        }

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            var order = repository.Orders.FirstOrDefault(x => x.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
    }
}