using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Migrations
{
    public partial class EuroStatusWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "automatedVehicle",
                table: "Vehicles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "euroStatus",
                table: "Vehicles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "realDrivingEmissions",
                table: "Vehicles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "revenueWeight",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "automatedVehicle",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "euroStatus",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "realDrivingEmissions",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "revenueWeight",
                table: "Vehicles");
        }
    }
}
