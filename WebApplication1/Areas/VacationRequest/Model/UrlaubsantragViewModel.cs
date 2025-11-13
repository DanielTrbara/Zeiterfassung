namespace WebApplication1.Areas.VacationRequest.Model;
using System.ComponentModel.DataAnnotations;

public class UrlaubsantragViewModel
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateFrom { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateTo { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Reason { get; set; }
    
    
}