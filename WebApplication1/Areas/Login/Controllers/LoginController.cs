using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
    }
}