using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Middleware.Data.Access.Migrations
{
    public partial class expandedmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(nullable: true),
                    DeviceName = table.Column<string>(nullable: true),
                    SatelliteCapable = table.Column<bool>(nullable: true),
                    SequenceNumber = table.Column<double>(nullable: false),
                    LastCom = table.Column<double>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    ComState = table.Column<int>(nullable: false),
                    Pac = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    DeviceTypeId = table.Column<string>(nullable: true),
                    GroupId = table.Column<string>(nullable: true),
                    Lqi = table.Column<int>(nullable: false),
                    ActivationTime = table.Column<double>(nullable: false),
                    TokenState = table.Column<int>(nullable: false),
                    TokenDetailMessage = table.Column<string>(nullable: true),
                    TokenEnd = table.Column<int>(nullable: false),
                    ContractId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<double>(nullable: false),
                    ModemCertificateId = table.Column<string>(nullable: true),
                    Prototype = table.Column<bool>(nullable: true),
                    ProductCertificateId = table.Column<string>(nullable: true),
                    AutomaticRenewal = table.Column<bool>(nullable: true),
                    AutomaticRenewalStatus = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastEditionTime = table.Column<double>(nullable: false),
                    LastEditedBy = table.Column<string>(nullable: true),
                    Activable = table.Column<bool>(nullable: true),
                    Payload = table.Column<byte[]>(nullable: true),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    RequestOrigin = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
