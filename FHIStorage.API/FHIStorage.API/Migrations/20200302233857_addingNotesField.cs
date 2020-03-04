using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class addingNotesField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Furniture",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Furniture");
        }
    }
}
