using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApp.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class add_vehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Make",
                table: "Vehicle",
                newName: "Brand");

            migrationBuilder.CreateTable(
                name: "VehiclePurchaseRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "品牌"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "型号"),
                    LicensePlateNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "车牌号"),
                    VIN = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false, comment: "车架号"),
                    EngineType = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "购买日期"),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "购买价格"),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "供应商名称"),
                    SupplierPhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false, comment: "供应商电话"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "支付方式"),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "备注"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclePurchaseRecords", x => x.Id);
                },
                comment: "车辆购买记录");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePurchaseRecord_VehicleId",
                table: "VehiclePurchaseRecords",
                column: "VehicleId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehiclePurchaseRecords");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Vehicle",
                newName: "Make");
        }
    }
}
