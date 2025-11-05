using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Krankschreibung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MitarbeiterName = table.Column<string>(type: "TEXT", nullable: false),
                    MitarbeiterEmail = table.Column<string>(type: "TEXT", nullable: false),
                    Von = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Bis = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MitAttest = table.Column<bool>(type: "INTEGER", nullable: false),
                    Kommentar = table.Column<string>(type: "TEXT", nullable: true),
                    ErfasstAm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Krankschreibung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LoginUsers",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", true, "$2a$11$zrTCmdAmPwGwA5YDE8JLe.NmjU7B7A9Ng9KWxkEimi8qdNlfJlnx2", 1, "admin" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", true, "$2a$11$v7swAPsNfVs53z2/kj7cvuB8fB1UV2u5DpQS71yphoim1NiX6GbES", 2, "hr" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Krankschreibung");

            migrationBuilder.DropTable(
                name: "LoginUsers");
        }
    }
}
