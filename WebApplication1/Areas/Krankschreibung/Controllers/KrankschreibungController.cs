using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Krankschreibung.Models;
using WebApplication1.Data;
using WebApplication1.Data.Models;

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

    [HttpPost]
    public async Task<IActionResult> Index(KrankschreibungViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var entity = new KrankschreibungModel();
        model.ConvertTo(entity);
        
        db.Krankschreibung.Add(entity);
        await db.SaveChangesAsync();
        
        TempData["SuccessMessage"] = "Krankschreibung wurde erfolgreich gespeichert.";
        
        return RedirectToAction("Index");

    }
}