using Ecommerce.Models;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class AcountsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcountsController(SignInManager<ApplicationUser> signInManager , UserManager<ApplicationUser> userManager)
        {
            
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public  async Task <IActionResult> LogIn(LoginVm model)
        {
            if (ModelState.IsValid) { 
                var result = await _signInManager.PasswordSignInAsync(model.Email , model.Password , true,true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index" , "Home");
                }
                else
                {
                    return View(model);
                }

            }


            return View(model);
        }
        
        public async Task <IActionResult> Registers(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Id=model.Id,
                  UserName=model.Email, 
                  Email=model.Email, 
                  Name = model.Name,    

                };
                user.Id=Guid.NewGuid().ToString();  
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    return View(model);

                }
            }
            return View(model);

        }
        public async Task <IActionResult> LogOut( )
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");   
        }

    }
}
