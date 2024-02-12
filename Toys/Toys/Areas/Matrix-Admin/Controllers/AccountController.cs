using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Toys.Areas.Matrix_Admin.ViewModels.Account;
using Toys.Models;

namespace Toys.Areas.Matrix_Admin.Controllers
{
    [Area("Matrix-admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("Username", "Not found");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Incorrect password");
                return View(model);
            }

            return RedirectToAction("index", "dashboard");
        }
    }
}
