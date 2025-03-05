using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApp.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class urchaseRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UK_VehiclePurchaseRecord_LicensePlate",
                table: "VehiclePurchaseRecords");

            migrationBuilder.DropIndex(
                name: "UK_VehiclePurchaseRecord_VIN",
                table: "VehiclePurchaseRecords");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "VehiclePurchaseRecords");

            migrationBuilder.DropColumn(
                name: "EngineType",
                table: "VehiclePurchaseRecords");

            migrationBuilder.DropColumn(
                name: "LicensePlateNumber",
                table: "VehiclePurchaseRecords");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "VehiclePurchaseRecords");

            migrationBuilder.DropColumn(
                name: "VIN",
                table: "VehiclePurchaseRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "VehiclePurchaseRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "品牌");

            migrationBuilder.AddColumn<int>(
                name: "EngineType",
                table: "VehiclePurchaseRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LicensePlateNumber",
                table: "VehiclePurchaseRecords",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                comment: "车牌号");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "VehiclePurchaseRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "型号");

            migrationBuilder.AddColumn<string>(
                name: "VIN",
                table: "VehiclePurchaseRecords",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                defaultValue: "",
                comment: "车架号");

            migrationBuilder.CreateIndex(
                name: "UK_VehiclePurchaseRecord_LicensePlate",
                table: "VehiclePurchaseRecords",
                column: "LicensePlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_VehiclePurchaseRecord_VIN",
                table: "VehiclePurchaseRecords",
                column: "VIN",
                unique: true);
        }
    }
}
