using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Areas.VacationRequest.Model;
using WebApplication1.Models;



namespace WebApplication1.Areas.VacationRequest.Controllers
{
    [Area("VacationRequest")]
    public class VacationRequestController(ILogger<VacationRequestController> logger, AppDbContext context)
        : Controller
    {
        
        [HttpGet]
        public IActionResult Index()
        {
            var model = new VacationRequestModel()
            {
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today.AddDays(1)
            };

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(VacationRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["PROBLEM"] = "Promleme ganz viele";
                return View(model);
            }

            // ✅ 1. Objekt fürs Speichern anlegen
                var request = new Models.VacationRequest();
                
                model.ConvertTo(request);

                // ✅ 2. In die Datenbank einfügen
                context.VacationRequests.Add(request);
                
                // ✅ 3. Änderungen speichern
                await context.SaveChangesAsync();

                // ✅ 4. Logging
                logger.LogInformation($"Urlaubsantrag von {model.DateFrom:d} bis {model.DateTo:d} gespeichert.");

                // ✅ 5. Erfolgsnachricht + Redirect
                TempData["SuccessMessage"] = "Ihr Urlaubsantrag wurde erfolgreich eingereicht!";
                return RedirectToAction("Index");
        }
    }
}