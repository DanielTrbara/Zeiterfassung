using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Models;

namespace WebApplication1.Areas.Krankschreibung.Models;

public class KrankschreibungViewModel
{
    public KrankschreibungViewModel(){}

    public KrankschreibungViewModel(KrankschreibungModel entity)
    {
        MitarbeiterName = entity.MitarbeiterName;
        MitarbeiterEmail = entity.MitarbeiterEmail;
        Von = entity.Von;
        Bis = entity.Bis;
        MitAttest = entity.MitAttest;
        Kommentar = entity.Kommentar;
    }
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
    public DateTime Von { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Bitte ein Enddatum angeben.")]
    [DataType(DataType.Date)]
    [Display(Name = "Bis")]
    public DateTime Bis { get; set; } = DateTime.Now;

    [Display(Name = "Ärztliches Attest vorhanden")]
    public bool MitAttest { get; set; }

    [Display(Name = "Kommentar / Anmerkung")]
    [DataType(DataType.MultilineText)]
    public string? Kommentar { get; set; }

    public void ConvertTo(KrankschreibungModel entity)
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
