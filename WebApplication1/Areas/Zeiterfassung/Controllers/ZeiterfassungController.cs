using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Zeiterfassung.Models;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Areas.Zeiterfassung.Controllers
{
    [Area("Zeiterfassung")]
    [Authorize] 
    public class ZeiterfassungController : Controller
    {
        private readonly AppDbContext _context;

        public ZeiterfassungController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Zeiterfassung()
        {
            var model = new ZeiterfassungViewModel();
            
            // Lade heutige Einträge aus der Datenbank
            var today = DateTime.Today;
            var todaysEntries = await _context.TimeEntries
                .Where(e => e.StartTime.Date == today)
                .OrderBy(e => e.StartTime)
                .ToListAsync();

            model.TodaysEntries = todaysEntries;
            model.TodaysTotalHours = todaysEntries.Sum(e => e.Hours);
            
        
            return View("Zeiterfassung", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTimeEntry(ZeiterfassungViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Berechne die Stunden
                var timeSpan = model.InputEndTime - model.InputStartTime;
                var hours = (decimal)timeSpan.TotalHours;

                // Bestimme die Kategorie basierend auf Beschreibung
                string category = "Working time";
                if (!string.IsNullOrEmpty(model.InputDescription))
                {
                    var desc = model.InputDescription.ToLower();
                    if (desc.Contains("break") || desc.Contains("pause"))
                    {
                        category = "Break time";
                    }
                    else if (desc.Contains("overtime") || desc.Contains("überstunden"))
                    {
                        category = "Overtime";
                    }
                }

                // Erstelle neuen TimeEntry
                var entry = new TimeEntry
                {
                    StartTime = model.InputStartTime,
                    EndTime = model.InputEndTime,
                    Description = model.InputDescription,
                    Category = category,
                    Hours = Math.Round(hours, 1),
                    TimeSpan = $"{model.InputStartTime:HH:mm} - {model.InputEndTime:HH:mm}"
                };

                _context.TimeEntries.Add(entry);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Zeiterfassung));
            }

            // Wenn ModelState ungültig, lade Daten neu
            var today = DateTime.Today;
            model.TodaysEntries = await _context.TimeEntries
                .Where(e => e.StartTime.Date == today)
                .OrderBy(e => e.StartTime)
                .ToListAsync();
            
            model.TodaysTotalHours = model.TodaysEntries.Sum(e => e.Hours);

            return View("Zeiterfassung", model);
        }
    }
    
}