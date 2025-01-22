using System.Diagnostics;
using BankAccount.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<ApplicationUser> userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> _userManager)
        {
            _logger = logger;
            userManager = _userManager;
        }

        public async  Task<IActionResult> Index()
        {
            var r = await userManager.GetUserAsync(User);
            //ViewBag.balance = r.Balance;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
