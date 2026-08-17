using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaalMaat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBronen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExterneBronnen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ToegevoegdOp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExterneBronnen", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExterneBronnen");
        }
    }
}
