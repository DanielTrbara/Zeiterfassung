using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Zeiterfassung.Controllers
{
    [Area("Zeiterfassung")]
    public class ZeiterfassungController : Controller
    {
        [HttpGet]
        public IActionResult Zeiterfassung()
        {
            return View("Zeiterfassung");
        }
    }
}