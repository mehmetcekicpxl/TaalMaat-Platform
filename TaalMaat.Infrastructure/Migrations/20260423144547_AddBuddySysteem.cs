using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaalMaat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBuddySysteem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuddyKoppelingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VrijwilligerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnderstaligId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GekoppeldOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActief = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuddyKoppelingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuddyKoppelingen_AspNetUsers_AnderstaligId",
                        column: x => x.AnderstaligId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuddyKoppelingen_AspNetUsers_VrijwilligerId",
                        column: x => x.VrijwilligerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuddyVerzoeken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VerzenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OntvangerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AangemaaktOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AfwijzingBericht = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuddyVerzoeken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuddyVerzoeken_AspNetUsers_OntvangerId",
                        column: x => x.OntvangerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuddyVerzoeken_AspNetUsers_VerzenderId",
                        column: x => x.VerzenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuddyKoppelingen_AnderstaligId",
                table: "BuddyKoppelingen",
                column: "AnderstaligId");

            migrationBuilder.CreateIndex(
                name: "IX_BuddyKoppelingen_VrijwilligerId",
                table: "BuddyKoppelingen",
                column: "VrijwilligerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuddyVerzoeken_OntvangerId",
                table: "BuddyVerzoeken",
                column: "OntvangerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuddyVerzoeken_VerzenderId",
                table: "BuddyVerzoeken",
                column: "VerzenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuddyKoppelingen");

            migrationBuilder.DropTable(
                name: "BuddyVerzoeken");
        }
    }
}
