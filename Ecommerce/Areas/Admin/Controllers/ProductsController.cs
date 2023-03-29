using Ecommerce.data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductsController : Controller
    {
        private readonly EcommerceContext _context;

        public ProductsController(EcommerceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products.Include(c=>c.Category).ToList();   
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product Model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/product", imageName);
                using (var stream = System.IO.File.Create(filePathImage))
                {
                    await File.CopyToAsync(stream);
                }
                Model.ProImage = imageName;
            }
            /*var Product = new Product
            {
                ProId = Model.ProId,
                CatId = Model.CatId,
                Price = Model.Price,
                Descreption = Model.Descreption,
                ProName = Model.ProName,
                ProImage = Model.ProImage,
            };*/
            _context.Add(Model);
            await _context.SaveChangesAsync();
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            var Products = _context.Products.Find(Id);
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");

            return View(Products);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, Product Model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/product", imageName);
                using (var stream = System.IO.File.Create(filePathImage))
                {
                    File.CopyTo(stream);
                }
                Model.ProImage = imageName;
            }
            else
            {
                Model.ProImage = Model.ProImage;
            }
            _context.Update(Model);
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                var Product = _context.Products.Find(Id);
                _context.Products.Remove(Product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));


            }
            return RedirectToAction(nameof(Index));

        }
    }
}
