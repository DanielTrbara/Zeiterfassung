using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Areas.VacationRequest.Model;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication1.Areas.VacationRequest.Controllers;
[Area("VacationRequest")]
[Authorize(Roles = "Admin,Hr")]

public class VacationRequestController : Controller
{
    private readonly ILogger<VacationRequestController> _logger;
        // private readonly IVacationService _vacationService; // Beispiel für einen Dienst

        // Konstruktor (für Dependency Injection)
        public VacationRequestController(ILogger<VacationRequestController> logger /*, IVacationService vacationService */)
        {
            _logger = logger;
            // _vacationService = vacationService;
        }

        // ===============================================
        // 1. GET-Anfrage: Seite anzeigen (Formular laden)
        // ===============================================
        
        // Diese Methode reagiert auf GET-Anfragen an /Vacation/Request
        [HttpGet]
        public IActionResult VacationRequest()
        {
            return View(new VacationRequestModel());
        }
        
        [HttpPost]
        public IActionResult Submit(VacationRequestModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            return RedirectToAction("Index");
        }

        // ===============================================
        // 2. POST-Anfrage: Formular verarbeiten (Antrag senden)
        // ===============================================
        
        // Diese Methode reagiert auf POST-Anfragen, wenn das Formular abgesendet wird
        // [HttpPost] ist wichtig, um es von der GET-Methode zu unterscheiden
        [HttpPost]
        [ValidateAntiForgeryToken] // Standard-Sicherheits-Token
        public IActionResult Request(VacationRequestModel model)
        {
            // Überprüft, ob das ViewModel die Validierungsregeln erfüllt (z.B. [Required]-Felder)
            if (ModelState.IsValid)
            {
                // HIER WÜRDE DIE GESCHÄFTSLOGIK FOLGEN:
                
                // 1. Daten speichern (z.B. an einen Datenbankdienst übergeben)
                // bool success = _vacationService.SubmitRequest(model);
                
                // 2. Logging
                _logger.LogInformation($"Urlaubsantrag von {model.DateFrom:d} bis {model.DateTo:d} erfolgreich empfangen.");

                // 3. Den Benutzer umleiten, um erneutes Absenden zu verhindern (PRG-Muster)
                // Oft wird hier auf eine Übersicht oder eine Bestätigungsseite umgeleitet
                TempData["SuccessMessage"] = "Ihr Urlaubsantrag wurde erfolgreich eingereicht!";
                return RedirectToAction("Overview", "Vacation"); 
            }

            // Wenn die Validierung fehlschlägt, wird das Formular erneut angezeigt,
            // wobei die fehlerhaften Eingaben und die Fehlermeldungen erhalten bleiben.
            _logger.LogWarning("Urlaubsantrag Validierung fehlgeschlagen.");
            return View(model);
        }
        
        // Beispiel-Aktion für die Umleitung
        public IActionResult Overview()
        {
            return View(); // Gibt die View /Views/Vacation/Overview.cshtml zurück
        }
}