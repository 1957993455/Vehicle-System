using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApp.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class initaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Stores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "市");

            migrationBuilder.AddColumn<string>(
                name: "Address_Detail",
                table: "Stores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "详细地址");

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "Stores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "区");

            migrationBuilder.AddColumn<string>(
                name: "Address_Province",
                table: "Stores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "省");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Stores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "街道");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Address_Detail",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Address_Province",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "Stores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "完整地址");
        }
    }
}
