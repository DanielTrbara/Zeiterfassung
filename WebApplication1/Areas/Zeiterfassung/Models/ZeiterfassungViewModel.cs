using System.ComponentModel.DataAnnotations;
using WebApplication1.Areas.Zeiterfassung.Models;

public class ZeiterfassungViewModel
{
    // =======================================================
    // A. Kacheln/Statistiken (Die oberste Zeile der Übersicht)
    // =======================================================

    public int ActiveEmployees { get; set; }
    public decimal TotalHoursWorked { get; set; }
    public int ScheduledMeetings { get; set; }
    public int EmployeesOnVacation { get; set; }
        
    // =======================================================
    // B. Zeiterfassungseingabe (Der linke untere Kasten)
    // =======================================================
        
    // Diese Properties werden für die Formular-Eingabe verwendet (Start/Ende/Beschreibung)
    [Required(ErrorMessage = "Bitte geben Sie eine Startzeit an.")]
    [DataType(DataType.Time)]
    [Display(Name = "Startzeit")]
    public DateTime InputStartTime { get; set; }

    [Required(ErrorMessage = "Bitte geben Sie eine Endzeit an.")]
    [DataType(DataType.Time)]
    [Display(Name = "Endzeit")]
    public DateTime InputEndTime { get; set; }

    [MaxLength(250)]
    [Display(Name = "Beschreibung (optional)")]
    public string InputDescription { get; set; } = string.Empty;


    // =======================================================
    // C. Heutige Arbeitszeit & Projekte (Die rechte Seite)
    // =======================================================

    // Liste der erfassten Zeiten für heute
    public List<Zeiterfassung> TodaysEntries { get; set; } = new List<Zeiterfassung>();
    public decimal TodaysTotalHours { get; set; }

    // Liste der Projekte
    public List<ProjektErfassung> ActiveProjects { get; set; } = new List<ProjektErfassung>();
}