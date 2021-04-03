using Microsoft.EntityFrameworkCore.Migrations;

namespace Middleware.Data.Access.Migrations
{
    public partial class newproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Acknowledgment",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LongPolling",
                table: "Devices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SeqNumber",
                table: "Devices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SigfoxDeviceTypeId",
                table: "Devices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acknowledgment",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "LongPolling",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "SeqNumber",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "SigfoxDeviceTypeId",
                table: "Devices");
        }
    }
}
