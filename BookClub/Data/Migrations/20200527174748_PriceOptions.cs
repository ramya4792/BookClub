using Microsoft.EntityFrameworkCore.Migrations;

namespace BookClub.Migrations
{
    public partial class PriceOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceOption",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PriceOption",
                table: "Books");
        }
    }
}
