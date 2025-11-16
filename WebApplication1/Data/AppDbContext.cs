using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Areas.Zeiterfassung.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<VacationRequest> VacationRequests => Set<VacationRequest>();
    public DbSet<LoginUser> LoginUsers => Set<LoginUser>();
    public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // >>>> Erzeuge DIR den Hash einmal mit:
        // cd ~/RiderProjects/Zeiterfassung/WebApplication1
        // dotnet run -- --make-hash
        // ...und füge ihn unten ein:

        var admin = new LoginUser
        {
            Id = 1,
            UserName = "admin",
            Email = "admin@example.com",
            Role = UserRoleEnum.Admin,
            IsActive = true,
            PasswordHash = "$2a$11$zrTCmdAmPwGwA5YDE8JLe.NmjU7B7A9Ng9KWxkEimi8qdNlfJlnx2"
        };

        var hr = new LoginUser
        {
            Id = 2,
            UserName = "hr",
            Role = UserRoleEnum.Hr,
            IsActive = true,
            PasswordHash = "$2a$11$v7swAPsNfVs53z2/kj7cvuB8fB1UV2u5DpQS71yphoim1NiX6GbES"
        };
        
        var supervisor = new LoginUser
        {
            Id = 3,
            UserName = "supervisor",
            Role = UserRoleEnum.Supervisor,
            IsActive = true,
            PasswordHash = "$2a$11$5MOf0rHb.dF5GH9Pxx3Wmu291U66Zup/2HgJ3BsKl90ZaLiNVqZmm"
        };

        var user = new LoginUser
        {
            Id = 4,
            UserName = "max",
            Role = UserRoleEnum.User,
            IsActive = true,
            PasswordHash = "$2a$11$rC/.iA9wdl4XLw9BhERh9OyCfmgzL.wymq8nxFNCVnL2beKFe8B8y"
        };

        modelBuilder.Entity<LoginUser>().HasData(admin,hr, supervisor, user);
    }

    
}
