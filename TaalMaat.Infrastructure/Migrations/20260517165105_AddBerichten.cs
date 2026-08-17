using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaalMaat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBerichten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Berichten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AfzenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OntvangerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Inhoud = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    VerzondenOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsGelezen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berichten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Berichten_AspNetUsers_AfzenderId",
                        column: x => x.AfzenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Berichten_AspNetUsers_OntvangerId",
                        column: x => x.OntvangerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_AfzenderId",
                table: "Berichten",
                column: "AfzenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_OntvangerId",
                table: "Berichten",
                column: "OntvangerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Berichten");
        }
    }
}
