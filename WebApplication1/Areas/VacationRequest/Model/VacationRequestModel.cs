namespace WebApplication1.Areas.VacationRequest.Model;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

public class VacationRequestModel
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

    public void ConvertTo(VacationRequest entity)
    {
        entity.DateFrom = DateFrom;
        entity.DateTo = DateTo;
        entity.Reason = Reason;
    }
}