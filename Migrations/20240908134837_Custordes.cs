using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakari.Migrations
{
    /// <inheritdoc />
    public partial class Custordes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Customer_CustomerId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_CustomerId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "OrderDetail");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Debtor",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Debtor",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "OrderDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_CustomerId",
                table: "OrderDetail",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Customer_CustomerId",
                table: "OrderDetail",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");
        }
    }
}
