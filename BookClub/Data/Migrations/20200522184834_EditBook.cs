using Microsoft.EntityFrameworkCore.Migrations;

namespace BookClub.Migrations
{
    public partial class EditBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_BookID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCategories_CategoryID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CategoryID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookCategoryID",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCategoryID",
                table: "Books",
                column: "BookCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCategories_BookCategoryID",
                table: "Books",
                column: "BookCategoryID",
                principalTable: "BookCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCategories_BookCategoryID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookCategoryID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookCategoryID",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookID",
                table: "Books",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_BookID",
                table: "Books",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCategories_CategoryID",
                table: "Books",
                column: "CategoryID",
                principalTable: "BookCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
