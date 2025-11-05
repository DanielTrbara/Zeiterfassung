using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Krankschreibung.Models;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Krankschreibung.Controllers;

[Area("Krankschreibung")]
public class KrankschreibungController(AppDbContext db) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var model = new KrankschreibungViewModel();
        return View(model);
    }
}