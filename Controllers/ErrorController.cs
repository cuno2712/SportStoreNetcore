using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Error()
        {
            return View();
        }
    }
}