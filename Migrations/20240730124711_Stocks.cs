using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakari.Migrations
{
    /// <inheritdoc />
    public partial class Stocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Transanction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpDated",
                table: "Stock",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Orderby",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Orderby",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transanction_CustomerId",
                table: "Transanction",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transanction_Customer_CustomerId",
                table: "Transanction",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transanction_Customer_CustomerId",
                table: "Transanction");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Transanction_CustomerId",
                table: "Transanction");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Transanction");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "LastUpDated",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Orderby",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Orderby",
                table: "Order");
        }
    }
}
