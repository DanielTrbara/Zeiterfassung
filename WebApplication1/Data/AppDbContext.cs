using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Areas.Zeiterfassung.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<LoginUser> LoginUsers => Set<LoginUser>();
    public DbSet<TimeEntry> TimeEntries {get; set;}
}   