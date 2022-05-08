using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCoursework.Data.Migrations
{
    public partial class produtstockontroller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_ProductID",
                table: "ProductStock",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_Product_ProductID",
                table: "ProductStock",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ProductID",
                table: "ProductStock");

            migrationBuilder.DropIndex(
                name: "IX_ProductStock_ProductID",
                table: "ProductStock");
        }
    }
}
