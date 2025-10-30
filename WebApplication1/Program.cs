using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models; 

var builder = WebApplication.CreateBuilder(args);

// --- App_Data sicherstellen + absoluten Pfad bauen ---
var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataDir);
var dbPath = Path.Combine(dataDir, "app.db");

// --- DbContext mit **absolutem** Pfad registrieren (umgeht Working-Dir-Probleme) ---
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));

// Services
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Routen
app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// --- Auto-Migration + Exception sichtbar ausgeben ---
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    
    if (!db.Set<LoginUser>().Any())
    {
        db.Add(new LoginUser
        {
            UserName = "test",
            PasswordHash = "1234"   // nur zum Testen, später hashen!
        });
        db.SaveChanges();
        Console.WriteLine("Test-User angelegt: test / 1234");
    }
    
}
catch (Exception ex)
{
    Console.WriteLine("DB init/migrate failed: " + ex);
    throw; // damit du den echten Stacktrace siehst
}



app.Run();