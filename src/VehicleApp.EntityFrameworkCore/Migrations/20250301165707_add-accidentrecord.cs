using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApp.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class addaccidentrecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccidentLocation",
                table: "VehicleAccidentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ClaimStatus",
                table: "VehicleAccidentRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "VehicleAccidentRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DriverLicenseType",
                table: "VehicleAccidentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "DriverViolation",
                table: "VehicleAccidentRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "VehicleAccidentRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandlingDepartment",
                table: "VehicleAccidentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceCompany",
                table: "VehicleAccidentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyNumber",
                table: "VehicleAccidentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportDate",
                table: "VehicleAccidentRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccidentLocation",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "ClaimStatus",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "DriverLicenseType",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "DriverViolation",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "HandlingDepartment",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "InsuranceCompany",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyNumber",
                table: "VehicleAccidentRecords");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "VehicleAccidentRecords");
        }
    }
}
