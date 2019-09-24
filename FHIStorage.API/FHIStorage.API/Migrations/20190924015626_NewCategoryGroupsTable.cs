using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class NewCategoryGroupsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryGroupId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryGroups",
                columns: table => new
                {
                    CategoryGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGroups", x => x.CategoryGroupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_HouseId",
                table: "Furniture",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Furniture_Houses_HouseId",
                table: "Furniture",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furniture_Houses_HouseId",
                table: "Furniture");

            migrationBuilder.DropTable(
                name: "CategoryGroups");

            migrationBuilder.DropIndex(
                name: "IX_Furniture_HouseId",
                table: "Furniture");

            migrationBuilder.DropColumn(
                name: "CategoryGroupId",
                table: "Categories");
        }
    }
}
