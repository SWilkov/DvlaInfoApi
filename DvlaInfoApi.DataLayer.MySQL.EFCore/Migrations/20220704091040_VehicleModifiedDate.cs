using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Migrations
{
    public partial class VehicleModifiedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "Vehicles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "dvlaInfos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "dvlaInfos");
        }
    }
}
