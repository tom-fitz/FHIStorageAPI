using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class addingNewColumnsToHouseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Houses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PointOfContact",
                table: "Houses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "PointOfContact",
                table: "Houses");
        }
    }
}
