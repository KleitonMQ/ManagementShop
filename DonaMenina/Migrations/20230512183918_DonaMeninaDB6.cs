using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonaMenina.Migrations
{
    /// <inheritdoc />
    public partial class DonaMeninaDB6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceDiscount",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MergeClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceDiscount",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MergeClasses");
        }
    }
}
