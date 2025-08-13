using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForeningWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddNyFunktion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donationer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MobilePayNummer = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Besked = table.Column<string>(type: "TEXT", nullable: true),
                    QrKodePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donationer", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donationer");
        }
    }
}
