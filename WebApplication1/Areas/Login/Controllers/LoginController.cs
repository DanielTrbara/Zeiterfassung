using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;

        public LoginController(AppDbContext db) => _db = db;

        [HttpGet]
        public IActionResult Login()
        {
            return View(new ViewModelLogin());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ViewModelLogin vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _db.Set<LoginUser>()
                .FirstOrDefaultAsync(u => u.UserName == vm.UserName && u.IsActive);

            if (user == null || user.PasswordHash != vm.Password)
            {
                ModelState.AddModelError("", "Benutzername oder Passwort ist falsch.");
                return View(vm);
            }

            // Nur Testausgabe — kein Cookie, keine Session
            TempData["LoginMessage"] = $"Willkommen, {user.UserName} ({user.Role})!";
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}