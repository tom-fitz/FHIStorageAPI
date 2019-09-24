using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class TwoNewFurnitureColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Furniture",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Furniture",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Furniture");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Furniture");
        }
    }
}
