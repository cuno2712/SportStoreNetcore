using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication7.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 5;

        // GET: /<controller>/
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = repository.Products.Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = string.IsNullOrEmpty(category)
                        ? repository.Products.Count()
                        : repository.Products.Where(x => x.Category == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}