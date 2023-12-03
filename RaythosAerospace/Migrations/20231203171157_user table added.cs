using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class usertableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_UserModel_UseId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_UserModel_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shipping_ShippingId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserModel_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipping",
                table: "Shipping");

            migrationBuilder.RenameTable(
                name: "UserModel",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Shipping",
                newName: "Shippings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shippings",
                table: "Shippings",
                column: "ShippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UseId",
                table: "Carts",
                column: "UseId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippings_ShippingId",
                table: "Orders",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "ShippingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UseId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippings_ShippingId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shippings",
                table: "Shippings");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserModel");

            migrationBuilder.RenameTable(
                name: "Shippings",
                newName: "Shipping");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipping",
                table: "Shipping",
                column: "ShippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_UserModel_UseId",
                table: "Carts",
                column: "UseId",
                principalTable: "UserModel",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_UserModel_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "UserModel",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shipping_ShippingId",
                table: "Orders",
                column: "ShippingId",
                principalTable: "Shipping",
                principalColumn: "ShippingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserModel_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
