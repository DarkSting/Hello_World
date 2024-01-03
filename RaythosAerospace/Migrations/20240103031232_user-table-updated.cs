using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaythosAerospace.Migrations
{
    public partial class usertableupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0000",
                column: "Color",
                value: "LIGHT GREEN");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0001",
                column: "Color",
                value: "LIGHT BLUE");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0002",
                column: "Color",
                value: "LIGHT PURPLE");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0003",
                column: "Color",
                value: "LIGHT PINK");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0004",
                column: "Color",
                value: "LIGHT GREEN");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0005",
                column: "Color",
                value: "BLACK");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0006",
                column: "Color",
                value: "RED");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0000",
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0001",
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0002",
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0003",
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0004",
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0005",
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: "C0006",
                column: "Color",
                value: null);
        }
    }
}
