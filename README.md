# 🧩 Datenbank-Workflow (SQLite + Entity Framework Core)

Dieses Projekt verwendet **Entity Framework Core** mit **SQLite** als lokale Datenbank.  
Jede Person im Team hat ihre **eigene lokale `app.db`**, die über **Migrationen** synchronisiert wird.

Damit die Datenbankstruktur bei allen identisch bleibt,  
müssen Änderungen **immer per Code und Migration** erfolgen – **niemals manuell in der Datenbank**.

---

## 🚀 Neue Tabelle anlegen

### 1️⃣ Neues Model erstellen

Lege eine neue Klasse in `Models/` an  
(oder in `Areas/<DeineArea>/Models/`, wenn die Tabelle nur dort gebraucht wird):

namespace WebApplication1.Models;

public class TaskEntry
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

### 2️⃣ Model im AppDbContext registrieren

Öffne die Datei Data/AppDbContext.cs und füge eine neue DbSet<> hinzu:
  public DbSet<TaskEntry> TaskEntries => Set<TaskEntry>();

### 3️⃣ Migration erstellen

Führe im Projektverzeichnis (wo deine .csproj liegt) folgenden Befehl aus:
  dotnet ef migrations add AddTaskEntry
💡 Der Name (AddTaskEntry) ist frei wählbar, aber bitte aussagekräftig benennen.
Dadurch wird im Ordner Migrations/ eine neue Datei angelegt, die deine Änderung beschreibt.

### 4️⃣ Datenbank aktualisieren

Anschließend:
  dotnet ef database update
Damit wird deine lokale Datenbank (App_Data/app.db) auf den neuesten Stand gebracht und enthält die neue Tabelle automatisch.

### 5️⃣ Änderungen committen & pushen
