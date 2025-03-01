using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApp.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class iniy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VehicleAggregateRootId",
                table: "VehiclePurchaseRecords",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Stores",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "门店描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldComment: "门店描述");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePurchaseRecords_VehicleAggregateRootId",
                table: "VehiclePurchaseRecords",
                column: "VehicleAggregateRootId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclePurchaseRecords_Vehicle_VehicleAggregateRootId",
                table: "VehiclePurchaseRecords",
                column: "VehicleAggregateRootId",
                principalTable: "Vehicle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehiclePurchaseRecords_Vehicle_VehicleAggregateRootId",
                table: "VehiclePurchaseRecords");

            migrationBuilder.DropIndex(
                name: "IX_VehiclePurchaseRecords_VehicleAggregateRootId",
                table: "VehiclePurchaseRecords");

            migrationBuilder.DropColumn(
                name: "VehicleAggregateRootId",
                table: "VehiclePurchaseRecords");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Stores",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                comment: "门店描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "门店描述");
        }
    }
}
