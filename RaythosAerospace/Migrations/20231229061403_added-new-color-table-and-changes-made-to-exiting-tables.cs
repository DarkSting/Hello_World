using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class addednewcolortableandchangesmadetoexitingtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_AirCraftId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Customization",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "AirCrafts");

            migrationBuilder.AddColumn<string>(
                name: "CustomizationId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorId",
                table: "AirCrafts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ColorModel",
                columns: table => new
                {
                    ColorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    availability = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorModel", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "CustomizationModel",
                columns: table => new
                {
                    CustomId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Scale = table.Column<double>(type: "float", nullable: false),
                    ExteriorColorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InteriorColorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeatId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExtraModifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomizationModel", x => x.CustomId);
                    table.ForeignKey(
                        name: "FK_CustomizationModel_ColorModel_ColorId",
                        column: x => x.ColorId,
                        principalTable: "ColorModel",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomizationModel_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "EngineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomizationModel_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ColorModel",
                columns: new[] { "ColorId", "Price", "availability" },
                values: new object[,]
                {
                    { "C0000", 3, true },
                    { "C0001", 3, true },
                    { "C0002", 3, true },
                    { "C0003", 3, true },
                    { "C0004", 3, true },
                    { "C0005", 3, true },
                    { "C0006", 3, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AirCraftId",
                table: "Products",
                column: "AirCraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomizationId",
                table: "Products",
                column: "CustomizationId",
                unique: true,
                filter: "[CustomizationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AirCrafts_ColorId",
                table: "AirCrafts",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizationModel_ColorId",
                table: "CustomizationModel",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizationModel_EngineId",
                table: "CustomizationModel",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizationModel_SeatId",
                table: "CustomizationModel",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirCrafts_ColorModel_ColorId",
                table: "AirCrafts",
                column: "ColorId",
                principalTable: "ColorModel",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CustomizationModel_CustomizationId",
                table: "Products",
                column: "CustomizationId",
                principalTable: "CustomizationModel",
                principalColumn: "CustomId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirCrafts_ColorModel_ColorId",
                table: "AirCrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CustomizationModel_CustomizationId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CustomizationModel");

            migrationBuilder.DropTable(
                name: "ColorModel");

            migrationBuilder.DropIndex(
                name: "IX_Products_AirCraftId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CustomizationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_AirCrafts_ColorId",
                table: "AirCrafts");

            migrationBuilder.DropColumn(
                name: "CustomizationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "AirCrafts");

            migrationBuilder.AddColumn<int>(
                name: "Customization",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "AirCrafts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AirCraftId",
                table: "Products",
                column: "AirCraftId",
                unique: true,
                filter: "[AirCraftId] IS NOT NULL");
        }
    }
}
