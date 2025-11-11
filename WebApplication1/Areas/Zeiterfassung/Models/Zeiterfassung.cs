namespace WebApplication1.Areas.Zeiterfassung.Models;
public class TimeEntry
{
    public int Id { get; set; } 
    
    public DateTime StartTime { get; set; } 
    public DateTime EndTime { get; set; }
    public string Description { get; set; } 

    public string Category { get; set; } 
    public decimal Hours { get; set; }
    public string TimeSpan { get; set; }
}
    
