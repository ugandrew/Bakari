using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakari.Migrations
{
    /// <inheritdoc />
    public partial class Cust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transanction_Customer_CustomerId",
                table: "Transanction");

            migrationBuilder.DropIndex(
                name: "IX_Transanction_CustomerId",
                table: "Transanction");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Transanction");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Transanction",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
