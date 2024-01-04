using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class phototableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirCraftPhoto",
                columns: table => new
                {
                    PhotoID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AirCraftID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirCraftPhoto", x => x.PhotoID);
                    table.ForeignKey(
                        name: "FK_AirCraftPhoto_AirCrafts_AirCraftID",
                        column: x => x.AirCraftID,
                        principalTable: "AirCrafts",
                        principalColumn: "AircraftId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirCraftPhoto_AirCraftID",
                table: "AirCraftPhoto",
                column: "AirCraftID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirCraftPhoto");
        }
    }
}
