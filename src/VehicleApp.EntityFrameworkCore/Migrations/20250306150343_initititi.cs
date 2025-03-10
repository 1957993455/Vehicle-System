using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApp.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class initititi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Vehicle",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Detail",
                table: "Vehicle",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "Vehicle",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Province",
                table: "Vehicle",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Vehicle",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Address_Detail",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Address_Province",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Vehicle");
        }
    }
}
