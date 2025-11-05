using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Krankschreibung.Models;

public class KrankschreibungViewModel
{
    [Required(ErrorMessage = "Bitte geben Sie den Namen des Mitarbeiters an.")]
    [Display(Name = "Mitarbeitername")]
    public string MitarbeiterName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie die E-Mail-Adresse an.")]
    [EmailAddress(ErrorMessage = "Bitte eine gültige E-Mail-Adresse eingeben.")]
    [Display(Name = "E-Mail-Adresse")]
    public string MitarbeiterEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte ein Startdatum angeben.")]
    [DataType(DataType.Date)]
    [Display(Name = "Von")]
    public DateTime Von { get; set; }

    [Required(ErrorMessage = "Bitte ein Enddatum angeben.")]
    [DataType(DataType.Date)]
    [Display(Name = "Bis")]
    public DateTime Bis { get; set; }

    [Display(Name = "Ärztliches Attest vorhanden")]
    public bool MitAttest { get; set; }

    [Display(Name = "Kommentar / Anmerkung")]
    [DataType(DataType.MultilineText)]
    public string? Kommentar { get; set; }

    public void ConvertTo(Data.Models.Krankschreibung entity)
    {
        entity.MitarbeiterName = MitarbeiterName;
        entity.MitarbeiterEmail = MitarbeiterEmail;
        entity.Von = Von;
        entity.Bis = Bis;
        entity.MitAttest = MitAttest;
        entity.Kommentar = Kommentar;
        entity.ErfasstAm = DateTime.Now;
    }
}
