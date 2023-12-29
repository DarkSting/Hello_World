using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class addednewpropertytocolortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirCrafts_ColorModel_ColorId",
                table: "AirCrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomizationModel_ColorModel_ColorId",
                table: "CustomizationModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomizationModel_Engines_EngineId",
                table: "CustomizationModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomizationModel_Seats_SeatId",
                table: "CustomizationModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CustomizationModel_CustomizationId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomizationModel",
                table: "CustomizationModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColorModel",
                table: "ColorModel");

            migrationBuilder.RenameTable(
                name: "CustomizationModel",
                newName: "Customization");

            migrationBuilder.RenameTable(
                name: "ColorModel",
                newName: "Colors");

            migrationBuilder.RenameIndex(
                name: "IX_CustomizationModel_SeatId",
                table: "Customization",
                newName: "IX_Customization_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomizationModel_EngineId",
                table: "Customization",
                newName: "IX_Customization_EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomizationModel_ColorId",
                table: "Customization",
                newName: "IX_Customization_ColorId");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customization",
                table: "Customization",
                column: "CustomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirCrafts_Colors_ColorId",
                table: "AirCrafts",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customization_Colors_ColorId",
                table: "Customization",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customization_Engines_EngineId",
                table: "Customization",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "EngineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customization_Seats_SeatId",
                table: "Customization",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "SeatID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customization_CustomizationId",
                table: "Products",
                column: "CustomizationId",
                principalTable: "Customization",
                principalColumn: "CustomId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirCrafts_Colors_ColorId",
                table: "AirCrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customization_Colors_ColorId",
                table: "Customization");

            migrationBuilder.DropForeignKey(
                name: "FK_Customization_Engines_EngineId",
                table: "Customization");

            migrationBuilder.DropForeignKey(
                name: "FK_Customization_Seats_SeatId",
                table: "Customization");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customization_CustomizationId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customization",
                table: "Customization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Colors");

            migrationBuilder.RenameTable(
                name: "Customization",
                newName: "CustomizationModel");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "ColorModel");

            migrationBuilder.RenameIndex(
                name: "IX_Customization_SeatId",
                table: "CustomizationModel",
                newName: "IX_CustomizationModel_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_Customization_EngineId",
                table: "CustomizationModel",
                newName: "IX_CustomizationModel_EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_Customization_ColorId",
                table: "CustomizationModel",
                newName: "IX_CustomizationModel_ColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomizationModel",
                table: "CustomizationModel",
                column: "CustomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColorModel",
                table: "ColorModel",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirCrafts_ColorModel_ColorId",
                table: "AirCrafts",
                column: "ColorId",
                principalTable: "ColorModel",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomizationModel_ColorModel_ColorId",
                table: "CustomizationModel",
                column: "ColorId",
                principalTable: "ColorModel",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomizationModel_Engines_EngineId",
                table: "CustomizationModel",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "EngineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomizationModel_Seats_SeatId",
                table: "CustomizationModel",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "SeatID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CustomizationModel_CustomizationId",
                table: "Products",
                column: "CustomizationId",
                principalTable: "CustomizationModel",
                principalColumn: "CustomId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
