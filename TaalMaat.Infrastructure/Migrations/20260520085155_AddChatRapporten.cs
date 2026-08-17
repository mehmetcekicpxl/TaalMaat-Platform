using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaalMaat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChatRapporten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRapporten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RapporteerderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GerapporteerdeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RapportageDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Toelichting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAfgehandeld = table.Column<bool>(type: "bit", nullable: false),
                    ToestemmingGegeven = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRapporten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRapporten_AspNetUsers_GerapporteerdeId",
                        column: x => x.GerapporteerdeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatRapporten_AspNetUsers_RapporteerderId",
                        column: x => x.RapporteerderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRapporten_GerapporteerdeId",
                table: "ChatRapporten",
                column: "GerapporteerdeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRapporten_RapporteerderId",
                table: "ChatRapporten",
                column: "RapporteerderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRapporten");
        }
    }
}
