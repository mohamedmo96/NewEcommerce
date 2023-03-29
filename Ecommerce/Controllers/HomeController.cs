using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ecommerce.data;
using Ecommerce.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceContext _context;

        public HomeController(ILogger<HomeController> logger,EcommerceContext context )
        {
            _logger = logger;
             _context = context;
        }

        public IActionResult Index()
        {
            var model = new IndexVM
            {
                Categories = _context.Categories.ToList(),
                Products = _context.Products.ToList()
            };
            return View(model);
        }

        public IActionResult Product()
        {
            var Product = _context.Products.ToList();
            return View(Product);
        }
        public IActionResult ProductCategory(int id )
        {
            var Product = _context.Products.Where(c=>c.CatId==id).ToList();
            return View(Product);
        }
        [HttpPost]
        public IActionResult SearchProduct(string NamePro)
        {
            var Product = _context.Products.Where(p=>p.ProName==NamePro).ToList();
            return View(Product);
        }


        public IActionResult ProductDetails(int? id)
        {
            var Product = _context.Products.Include(x=>x.Category).FirstOrDefault (p=>p.ProId==id);
            return View(Product);
        }

        public IActionResult CartPaid()
        {
            return View();
        }

        public IActionResult Connect(Connect model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");   
            }
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    
}