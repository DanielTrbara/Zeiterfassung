namespace WebApplication1.Areas.Zeiterfassung.Models;

public class Zeiterfassung
{
    public string Category { get; set; } // z.B. "Working Time", "Break Time", "Overtime"
    public string TimeSpan { get; set; } // z.B. "8:00 - 13:00"
    public decimal Hours { get; set; }   // z.B. 5 oder 1.5
}