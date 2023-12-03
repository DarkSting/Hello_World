using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    EngineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EngineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    UnitCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.EngineId);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatID);
                });

            migrationBuilder.CreateTable(
                name: "AirCrafts",
                columns: table => new
                {
                    AircraftId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AircraftType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfManufacture = table.Column<int>(type: "int", nullable: false),
                    SeatingCapacity = table.Column<int>(type: "int", nullable: false),
                    MaximumRange = table.Column<double>(type: "float", nullable: false),
                    EngineTypeEngineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaxAllowedEngines = table.Column<int>(type: "int", nullable: false),
                    FuelCapacity = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    MaxSeatesAllowed = table.Column<int>(type: "int", nullable: false),
                    MaxWingSpan = table.Column<int>(type: "int", nullable: false),
                    ManfacturedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirCrafts", x => x.AircraftId);
                    table.ForeignKey(
                        name: "FK_AirCrafts_Engines_EngineTypeEngineId",
                        column: x => x.EngineTypeEngineId,
                        principalTable: "Engines",
                        principalColumn: "EngineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "EngineId", "EngineType", "UnitCount", "UnitPrice" },
                values: new object[,]
                {
                    { "E0000", "Turbojet", 6, 1000 },
                    { "E0001", "Turbofan", 6, 1000 },
                    { "E0002", "Turboprop", 6, 1000 },
                    { "E0003", "Turboshaft", 6, 1000 },
                    { "E0004", "Piston", 6, 1000 },
                    { "E0005", "Ramjet", 6, 1000 },
                    { "E0006", "HybridElectric", 6, 1000 },
                    { "E0007", "Other", 6, 1000 }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatID", "SeatCount", "SeatType", "UnitPrice" },
                values: new object[,]
                {
                    { "S0000", 10, "Economy", 200 },
                    { "S0001", 10, "PremiumEconomy", 200 },
                    { "S0002", 10, "Business", 200 },
                    { "S0003", 10, "FirstClass", 200 },
                    { "S0004", 10, "Other", 200 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirCrafts_EngineTypeEngineId",
                table: "AirCrafts",
                column: "EngineTypeEngineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirCrafts");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Engines");
        }
    }
}
