using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class addedrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirCrafts_Engines_EngineTypeEngineId",
                table: "AirCrafts");

            migrationBuilder.RenameColumn(
                name: "EngineTypeEngineId",
                table: "AirCrafts",
                newName: "SeatID");

            migrationBuilder.RenameIndex(
                name: "IX_AirCrafts_EngineTypeEngineId",
                table: "AirCrafts",
                newName: "IX_AirCrafts_SeatID");

            migrationBuilder.AddColumn<string>(
                name: "EngineId",
                table: "AirCrafts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AirCrafts_EngineId",
                table: "AirCrafts",
                column: "EngineId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirCrafts_Engines_EngineId",
                table: "AirCrafts",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "EngineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AirCrafts_Seats_SeatID",
                table: "AirCrafts",
                column: "SeatID",
                principalTable: "Seats",
                principalColumn: "SeatID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirCrafts_Engines_EngineId",
                table: "AirCrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_AirCrafts_Seats_SeatID",
                table: "AirCrafts");

            migrationBuilder.DropIndex(
                name: "IX_AirCrafts_EngineId",
                table: "AirCrafts");

            migrationBuilder.DropColumn(
                name: "EngineId",
                table: "AirCrafts");

            migrationBuilder.RenameColumn(
                name: "SeatID",
                table: "AirCrafts",
                newName: "EngineTypeEngineId");

            migrationBuilder.RenameIndex(
                name: "IX_AirCrafts_SeatID",
                table: "AirCrafts",
                newName: "IX_AirCrafts_EngineTypeEngineId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirCrafts_Engines_EngineTypeEngineId",
                table: "AirCrafts",
                column: "EngineTypeEngineId",
                principalTable: "Engines",
                principalColumn: "EngineId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
