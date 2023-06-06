using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementShop.Migrations
{
    /// <inheritdoc />
    public partial class ManagementDB01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceDiscount = table.Column<int>(type: "int", nullable: false),
                    TotalSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdm = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerId);
                });

            migrationBuilder.CreateTable(
                name: "MergeClasses",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    WorkerID = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantitySold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MergeClasses", x => new { x.SaleId, x.WorkerID, x.ProductId });
                    table.ForeignKey(
                        name: "FK_MergeClasses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MergeClasses_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MergeClasses_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MergeClasses_ProductId",
                table: "MergeClasses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MergeClasses_WorkerID",
                table: "MergeClasses",
                column: "WorkerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MergeClasses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
