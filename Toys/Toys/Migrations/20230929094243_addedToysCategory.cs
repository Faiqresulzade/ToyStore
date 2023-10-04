using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toys.Migrations
{
    public partial class addedToysCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toys_ToysCategory_Toys_CategoryId",
                table: "Toys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToysCategory",
                table: "ToysCategory");

            migrationBuilder.RenameTable(
                name: "ToysCategory",
                newName: "ToysCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToysCategories",
                table: "ToysCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Toys_ToysCategories_Toys_CategoryId",
                table: "Toys",
                column: "Toys_CategoryId",
                principalTable: "ToysCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toys_ToysCategories_Toys_CategoryId",
                table: "Toys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToysCategories",
                table: "ToysCategories");

            migrationBuilder.RenameTable(
                name: "ToysCategories",
                newName: "ToysCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToysCategory",
                table: "ToysCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Toys_ToysCategory_Toys_CategoryId",
                table: "Toys",
                column: "Toys_CategoryId",
                principalTable: "ToysCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
