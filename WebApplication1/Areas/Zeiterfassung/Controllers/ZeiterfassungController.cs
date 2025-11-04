using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Zeiterfassung.Models; 

namespace WebApplication1.Areas.Zeiterfassung.Controllers
{
    [Area("Zeiterfassung")]
    public class ZeiterfassungController : Controller
    {
        [HttpGet]
        public IActionResult Zeiterfassung()
        {
            var viewModel = new ZeiterfassungViewModel();
            return View("Zeiterfassung", viewModel); 
        }
    }
}