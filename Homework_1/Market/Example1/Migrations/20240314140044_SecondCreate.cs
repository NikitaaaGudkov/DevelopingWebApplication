using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example1.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ProductName",
                table: "Categories",
                newName: "IX_Categories_CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "ProductName");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                newName: "IX_Categories_ProductName");
        }
    }
}
