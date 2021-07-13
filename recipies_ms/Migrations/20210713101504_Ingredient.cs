using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class Ingredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recipe_ingredient",
                columns: table => new
                {
                    ingredient_key = table.Column<Guid>(type: "uuid", nullable: false),
                    recipe_key = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: false),
                    ingredient = table.Column<string>(type: "text", nullable: false),
                    ingredient_note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_ingredient", x => x.ingredient_key);
                    table.ForeignKey(
                        name: "FK_recipe_ingredient_recipe_recipe_key",
                        column: x => x.recipe_key,
                        principalTable: "recipe",
                        principalColumn: "recipe_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredient_recipe_key",
                table: "recipe_ingredient",
                column: "recipe_key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipe_ingredient");
        }
    }
}
