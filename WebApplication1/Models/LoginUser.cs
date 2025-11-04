namespace WebApplication1.Models;

public class LoginUser
{
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public UserRoleEnum Role { get; set; }
}