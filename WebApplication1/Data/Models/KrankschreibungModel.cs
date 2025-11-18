namespace WebApplication1.Data.Models;

public class KrankschreibungModel
{
    public int Id { get; set; }

    // Wer ist krank
    public string MitarbeiterName { get; set; } = string.Empty;
    public string MitarbeiterEmail { get; set; } = string.Empty;

    // Zeitraum
    public DateTime Von { get; set; }
    public DateTime Bis { get; set; }

    // Metadaten
    public bool MitAttest { get; set; }
    public string? Kommentar { get; set; }
    public DateTime ErfasstAm { get; set; } = DateTime.Now;
}