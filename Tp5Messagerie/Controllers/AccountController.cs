using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tp5Messagerie.Entities;
using Tp5Messagerie.ViewModels.Account;

namespace Tp5Messagerie.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult LogIn(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LogInVM vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var result = await _signInManager.PasswordSignInAsync(
                    vm.UserName, vm.Password, false, false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "LogIn Failed!");
                    return View(vm);
                }

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");

            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return RedirectToAction("LogIn", "Account");

            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
