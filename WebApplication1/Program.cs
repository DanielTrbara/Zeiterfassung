using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;
using WebApplication1.Models; 
using BCrypt.Net;

// --- Nur ausführen, wenn du explizit das Flag übergibst: `dotnet run -- --make-hash`
if (args.Contains("--make-hash"))
{
    Console.Write("Passwort eingeben: ");
    var pw = Console.ReadLine() ?? string.Empty;
    if (string.IsNullOrWhiteSpace(pw))
    {
        Console.WriteLine("Kein Passwort eingegeben.");
        return;
    }

    var hash = PasswordHasher.Generate(pw);
    Console.WriteLine("\nHash:\n" + hash);
    return; // danach NICHT die Web-App starten
}

var builder = WebApplication.CreateBuilder(args);

// --- App_Data sicherstellen + absoluten Pfad bauen ---
var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataDir);
var dbPath = Path.Combine(dataDir, "app.db");

// --- DbContext registrieren ---
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));

// Services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ---- Simple DB seed (nur Dev) ----
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();

    if (!await db.Set<LoginUser>().AnyAsync())
    {
        var admin = new LoginUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            Role = UserRoleEnum.Admin,
            IsActive = true,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123") 
        };
        await db.AddAsync(admin);
        await db.SaveChangesAsync();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();


// -- Helper für BCrypt-Hashes (nur lokal zum Erzeugen von Hashes) --
static class PasswordHasher
{
    public static string Generate(string password) => BCrypt.Net.BCrypt.HashPassword(password);
}