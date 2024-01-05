using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class updatedproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ComponentAssemblyStatus",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveredStatus",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignEngineeringStatus",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderProcessingStatus",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrototypingTestingStatus",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippedStatus",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestingCertificationStatus",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentAssemblyStatus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeliveredStatus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DesignEngineeringStatus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderProcessingStatus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PrototypingTestingStatus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShippedStatus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TestingCertificationStatus",
                table: "Products");

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
    }
}
