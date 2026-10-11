using DAL_Arch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_With_PL_Arch.Helpers;
using MVC_With_PL_Arch.ViewModels;
using System.Threading.Tasks;

namespace MVC_With_PL_Arch.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel RegisterVM)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = RegisterVM.Email.Split('@')[0],
                    Email = RegisterVM.Email,
                    FName = RegisterVM.FName,
                    LName = RegisterVM.LName,
                    IsAgree = RegisterVM.IsAgree


                };
                var result= await _userManager.CreateAsync(User , RegisterVM.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        
                    }
                }

            }


            return View(RegisterVM);
        }
        #endregion


        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var User= await _userManager.FindByEmailAsync(loginVM.Email);
                if (User is not null)
                { 
                    var Result= await _userManager.CheckPasswordAsync(User, loginVM.Password);
                    if (Result)
                    { 
                        var LoginResult= await _signInManager.PasswordSignInAsync(User,loginVM.Password, loginVM.RememberMe,false);
                        if(LoginResult.Succeeded)
                            return RedirectToAction("Index", "Home");   
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Password is incorrect");
                }
                else
                    ModelState.AddModelError(string.Empty, "Email is not found");
            }
            return View(loginVM);
        }
        #endregion

        #region LogOut
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();//to remove token from cooke scheme
            return RedirectToAction(nameof(Login));
        }
        #endregion

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel forgetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(forgetPasswordVM.Email);
                if (User is not null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(User);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = forgetPasswordVM.Email ,Token=token }, Request.Scheme);
                    var Email = new Email()
                    {
                        To = User.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink
                    };
                    EmailSettings.SendEmail(Email);
                    return RedirectToAction(nameof(CheckYourInbox));


                }
                else
                    ModelState.AddModelError(string.Empty, "Email is not found");
            }
            
            return View("ForgetPassword" ,forgetPasswordVM);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
