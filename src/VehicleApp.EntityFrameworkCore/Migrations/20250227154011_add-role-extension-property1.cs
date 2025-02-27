using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApp.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class addroleextensionproperty1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisaplayName",
                table: "AbpRoles",
                newName: "DisplayName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "AbpRoles",
                newName: "DisaplayName");
        }
    }
}
