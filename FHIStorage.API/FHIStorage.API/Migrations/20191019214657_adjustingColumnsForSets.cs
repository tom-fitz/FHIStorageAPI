using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class adjustingColumnsForSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "FurnitureSets");

            migrationBuilder.RenameColumn(
                name: "MasterFurnitureId",
                table: "FurnitureSets",
                newName: "Quantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "FurnitureSets",
                newName: "MasterFurnitureId");

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "FurnitureSets",
                nullable: false,
                defaultValue: 0);
        }
    }
}
