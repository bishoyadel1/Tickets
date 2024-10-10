using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tickets.EmailConfig;
using Tickets.Models;

namespace Tickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager ,RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            this.roleManager = _roleManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ResetPassword(string email , string token)
        {
            return View();
        }

        [HttpPost]
        [Route("Account/ResetPassword")]
        public async Task<IActionResult> ResetPassword( ResetPasswordViewModel newpass )
        {

            if(!string.IsNullOrEmpty(newpass.Email) && !string.IsNullOrEmpty(newpass.Token) && ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(newpass.Email);
                if(user != null)
                {
                    var newpassword = await userManager.ResetPasswordAsync(user, newpass.Token, newpass.Password);
                    if(newpassword.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Invaild Password");


                }
            }
            return View(newpass);
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email  , token = token },Request.Scheme);
                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = url,
                        To = model.Email
                    };
                    EmailSetting.SendMail(email);
                    return RedirectToAction("Login");
                }


                return RedirectToAction("ForgetPassword");
            }
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

                        var userNow = await userManager.GetUserAsync(User);
                      
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
                if(model.IsOrganizer is true)
                     await userManager.AddToRoleAsync(user, "Organizer");
              

                if (result.Succeeded )
                {
                    if (userManager.Users.Count() == 1)
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                        await roleManager.CreateAsync(new IdentityRole { Name = "Organizer" });
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
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
