using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonaMenina.Migrations
{
    /// <inheritdoc />
    public partial class DonameninaDB7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MergeClasses_Products_ProductId1",
                table: "MergeClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_MergeClasses_Sales_SaleId1",
                table: "MergeClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Workers_WorkerId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_WorkerId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MergeClasses",
                table: "MergeClasses");

            migrationBuilder.DropIndex(
                name: "IX_MergeClasses_ProductId1",
                table: "MergeClasses");

            migrationBuilder.DropIndex(
                name: "IX_MergeClasses_SaleId1",
                table: "MergeClasses");

            migrationBuilder.DropColumn(
                name: "IdWorker",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "MergeClasses");

            migrationBuilder.DropColumn(
                name: "SaleId1",
                table: "MergeClasses");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "MergeClasses",
                newName: "QuantitySold");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataSale",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WorkerID",
                table: "MergeClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MergeClasses",
                table: "MergeClasses",
                columns: new[] { "SaleId", "WorkerID", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_MergeClasses_WorkerID",
                table: "MergeClasses",
                column: "WorkerID");

            migrationBuilder.AddForeignKey(
                name: "FK_MergeClasses_Workers_WorkerID",
                table: "MergeClasses",
                column: "WorkerID",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MergeClasses_Workers_WorkerID",
                table: "MergeClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MergeClasses",
                table: "MergeClasses");

            migrationBuilder.DropIndex(
                name: "IX_MergeClasses_WorkerID",
                table: "MergeClasses");

            migrationBuilder.DropColumn(
                name: "DataSale",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "WorkerID",
                table: "MergeClasses");

            migrationBuilder.RenameColumn(
                name: "QuantitySold",
                table: "MergeClasses",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "IdWorker",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "MergeClasses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleId1",
                table: "MergeClasses",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MergeClasses",
                table: "MergeClasses",
                columns: new[] { "SaleId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_WorkerId",
                table: "Sales",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MergeClasses_ProductId1",
                table: "MergeClasses",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_MergeClasses_SaleId1",
                table: "MergeClasses",
                column: "SaleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MergeClasses_Products_ProductId1",
                table: "MergeClasses",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_MergeClasses_Sales_SaleId1",
                table: "MergeClasses",
                column: "SaleId1",
                principalTable: "Sales",
                principalColumn: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Workers_WorkerId",
                table: "Sales",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
