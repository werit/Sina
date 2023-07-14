using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class IngredietnNutrition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingredient_nutrition",
                columns: table => new
                {
                    nutrition_key = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: false),
                    fat_content_percentage = table.Column<float>(type: "real", nullable: false),
                    saccharides_content_percentage = table.Column<float>(type: "real", nullable: false),
                    protein_content_percentage = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient_nutrition", x => x.nutrition_key);
                    table.ForeignKey(
                        name: "FK_ingredient_nutrition_ingredient_nutrition_key",
                        column: x => x.nutrition_key,
                        principalTable: "ingredient",
                        principalColumn: "ingredient_key",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingredient_nutrition");
        }
    }
}
