using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class NewDBFurnitureSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFurnitureSet",
                table: "Furniture",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Furniture",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FurnitureSets",
                columns: table => new
                {
                    FurnitureSetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FurnitureId = table.Column<int>(nullable: false),
                    HouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureSets", x => x.FurnitureSetId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FurnitureSets");

            migrationBuilder.DropColumn(
                name: "IsFurnitureSet",
                table: "Furniture");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Furniture");
        }
    }
}
