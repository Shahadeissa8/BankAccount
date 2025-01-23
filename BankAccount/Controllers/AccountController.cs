using BankAccount.Models;
using BankAccount.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        private SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.Mobile,
                    Balance = 1000
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                Console.WriteLine("Username: " + model.Name);
                Console.WriteLine("Password: " + model.Password);
                Console.WriteLine("RemeberMe: " + model.RememberMe);
                var result = await signInManager.PasswordSignInAsync(userName:model.Name, model.Password, false, false);
                if (result.Succeeded)
                {
                    var r = await userManager.GetUserAsync(User);
                    HttpContext.Session.SetString("Balance", r.Balance.ToString());
                    HttpContext.Session.SetString("Name", r.UserName.ToString());
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid user or password");
                return View(model);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {

            var r = await userManager.GetUserAsync(User);
            HttpContext.Session.SetString("Balance", r.Balance.ToString());
            HttpContext.Session.SetString("Name", r.UserName.ToString());

            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmLogout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
