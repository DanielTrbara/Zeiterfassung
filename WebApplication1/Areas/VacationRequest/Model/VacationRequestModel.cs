using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.VacationRequest.Model;
using System.ComponentModel.DataAnnotations;

[Area("VacationRequest")]
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
}