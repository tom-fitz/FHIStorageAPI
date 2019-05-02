using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FHIStorage.API.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    FurnitureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    UID = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    PurchasedFrom = table.Column<string>(nullable: true),
                    DatePurchased = table.Column<DateTime>(nullable: true),
                    HouseId = table.Column<int>(nullable: false),
                    Turns = table.Column<int>(nullable: false),
                    FurnitureImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.FurnitureId);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    HouseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Zipcode = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    ContractDate = table.Column<DateTime>(nullable: false),
                    DateSold = table.Column<DateTime>(nullable: true),
                    Sold = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.HouseId);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureImages",
                columns: table => new
                {
                    FurnitureImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FurnitureId = table.Column<int>(nullable: false),
                    PictureInfo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureImages", x => x.FurnitureImageId);
                    table.ForeignKey(
                        name: "FK_FurnitureImages_Furniture_FurnitureId",
                        column: x => x.FurnitureId,
                        principalTable: "Furniture",
                        principalColumn: "FurnitureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureImages_FurnitureId",
                table: "FurnitureImages",
                column: "FurnitureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "FurnitureImages");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Furniture");
        }
    }
}
