using System.ComponentModel.DataAnnotations;

public class ViewModelLogin
{
    [Required(ErrorMessage = "Bitte geben sie ihren Benutzernamen ein.")]
    [Display(Name = "Benutzernamen")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Bitte geben sie ihr Passwort ein.")]
    [DataType(DataType.Password)]
    [Display(Name = "Passwort")]
    public string Password { get; set; }
}