using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaalMaat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSessies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VrijwilligerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnderstaligId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GeplandOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JitsiUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsBevestigd = table.Column<bool>(type: "bit", nullable: false),
                    AangemaaktOp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessies_AspNetUsers_AnderstaligId",
                        column: x => x.AnderstaligId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessies_AspNetUsers_VrijwilligerId",
                        column: x => x.VrijwilligerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessies_AnderstaligId",
                table: "Sessies",
                column: "AnderstaligId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessies_VrijwilligerId",
                table: "Sessies",
                column: "VrijwilligerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessies");
        }
    }
}
