using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tickets.Models;

namespace Tickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null) {
                    var result = await userManager.CheckPasswordAsync(user, model.Password);
                    if (result)
                    {
                        await signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError(string.Empty,"Invaild Password");

                }
           

            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    

                };
                     
              var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var Error in result.Errors)
                        ModelState.AddModelError(string.Empty, Error.Description);
                    
                }



            }
            return View(model);
        }


    }
}
