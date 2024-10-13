using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageRole()
        {
            var users = userManager.Users.ToList();
            var userRoles = new List<UsersRoleViewModel>();

            foreach (var user in users)
            {
                var Admin1 = await userManager.IsInRoleAsync(user, "Admin");
                var Organizer1 = await userManager.IsInRoleAsync(user, "Organizer");
                var userRole = new UsersRoleViewModel { Email= user.Email, Id=user.Id,Name=user.UserName, IsAdmin = Admin1 , IsOrganizer =Organizer1 };
                userRoles.Add(userRole);
            }
          

            return View(userRoles);
        }


        public async Task<IActionResult> ShowUserProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var profileViewModel = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };
            return View(profileViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var profileViewModel = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };
            return View(profileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.Phone;
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return View(model);
            }
            return RedirectToAction(nameof(ShowUserProfile));
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRoleFromUser(string UserId , string rolename)
        {
            var user = await userManager.FindByIdAsync(UserId);
            var role = await roleManager.FindByNameAsync(rolename);

            if (user != null && role != null)
            {
             await userManager.RemoveFromRoleAsync(user, role.Name);

                    return RedirectToAction("ManageRole");
       
            }
           
               return RedirectToAction("ManageRole");

 
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser(string UserId, string rolename)
        {
            var user = await userManager.FindByIdAsync(UserId);
            var role = await roleManager.FindByNameAsync(rolename);

            if (user != null && role != null)
            {
                await userManager.AddToRoleAsync(user, rolename);
                return RedirectToAction("ManageRole");
            }

            return RedirectToAction("ManageRole");


        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var user = new IdentityUser()
                {
                    
                    Email = model.Email,
                    UserName = model.Name,
                    

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


        public async Task<IActionResult> Logout()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }
    }
}
