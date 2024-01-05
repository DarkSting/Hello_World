using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class ordertableupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ComponentAssemblyStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveredStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignEngineeringStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MyProperty",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderProcessingStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrototypingTestingStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippedStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestingCertificationStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentAssemblyStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveredStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DesignEngineeringStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderProcessingStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PrototypingTestingStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippedStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TestingCertificationStatus",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
