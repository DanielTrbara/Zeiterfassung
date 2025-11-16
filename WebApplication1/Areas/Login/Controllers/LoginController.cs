using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;

        public LoginController(AppDbContext db) => _db = db;

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new ViewModelLogin());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ViewModelLogin vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _db.Set<LoginUser>()
                .FirstOrDefaultAsync(u => u.UserName == vm.UserName && u.IsActive);

            var ok = user != null && BCrypt.Net.BCrypt.Verify(vm.Password, user.PasswordHash);
            if (!ok)
            {
                ModelState.AddModelError("", "Benutzername oder Passwort ist falsch.");
                return View(vm);
            }

            // >>> HIER NEU: altes Cookie löschen, bevor wir neu einloggen
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // === Claims + Cookie setzen ===
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())   // "Admin", "Hr", ...
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });

            return RedirectToAction("Zeiterfassung", "Zeiterfassung", new { area = "Zeiterfassung" });
        }
        
        [Authorize] // darf nur aufgerufen werden, wenn jemand eingeloggt ist
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login", new { area = "Login" });
        }
    }
}