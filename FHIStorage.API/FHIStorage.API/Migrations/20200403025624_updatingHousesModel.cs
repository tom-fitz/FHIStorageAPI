using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class updatingHousesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Houses",
                newName: "ContractedPrice");

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Houses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Town",
                table: "Houses");

            migrationBuilder.RenameColumn(
                name: "ContractedPrice",
                table: "Houses",
                newName: "Cost");
        }
    }
}
