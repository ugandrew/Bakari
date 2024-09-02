using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakari.Migrations
{
    /// <inheritdoc />
    public partial class Bas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasketId1",
                table: "Basket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Basket_BasketId1",
                table: "Basket",
                column: "BasketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Basket_BasketId1",
                table: "Basket",
                column: "BasketId1",
                principalTable: "Basket",
                principalColumn: "BasketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Basket_BasketId1",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_BasketId1",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "BasketId1",
                table: "Basket");
        }
    }
}
