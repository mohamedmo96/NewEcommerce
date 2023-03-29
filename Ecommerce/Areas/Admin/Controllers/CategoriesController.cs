using Ecommerce.data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly EcommerceContext _context;

        public CategoriesController(EcommerceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category Model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/category", imageName);
                using (var stream = System.IO.File.Create(filePathImage))
                {
                    await File.CopyToAsync(stream);
                }
                Model.CatPhoto = imageName;
            }
            _context.Add(Model); 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit( int ? Id)
        {
            var categories = _context.Categories.Find(Id);      
            return View(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Edit( int Id , Category Model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/category", imageName);
                using (var stream = System.IO.File.Create(filePathImage))
                {
                     File.CopyTo(stream);
                }
                Model.CatPhoto = imageName;
            }
            else
            {
                Model.CatPhoto = Model.CatPhoto;    
            }
            _context.Update(Model);
          await   _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete( int ? Id)
        {
            if (Id != null)
            {
                var Cat = _context.Categories.Find(Id);
               _context.Categories.Remove(Cat); 
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));


            }
            return View();

        }
    }
}
