using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class removingZipCodeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "Houses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Zipcode",
                table: "Houses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
