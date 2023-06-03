using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonaMenina.Migrations
{
    /// <inheritdoc />
    public partial class DonaMeninaDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsManage",
                table: "Workers",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Workers",
                newName: "IsManage");
        }
    }
}
