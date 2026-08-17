using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaalMaat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOefeningen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oefeningen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Inhoud = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YouTubeUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AudioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Niveau = table.Column<int>(type: "int", nullable: false),
                    IsGoedgekeurd = table.Column<bool>(type: "bit", nullable: false),
                    AangemaaktOp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oefeningen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OefeningVragen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OefeningId = table.Column<int>(type: "int", nullable: false),
                    VraagTekst = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    JuistAntwoord = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OptiesJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OefeningVragen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OefeningVragen_Oefeningen_OefeningId",
                        column: x => x.OefeningId,
                        principalTable: "Oefeningen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OefeningVragen_OefeningId",
                table: "OefeningVragen",
                column: "OefeningId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OefeningVragen");

            migrationBuilder.DropTable(
                name: "Oefeningen");
        }
    }
}
