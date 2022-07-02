using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class AddSeparateIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_ingredient_recipe_recipe_key",
                table: "recipe_ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_ingredient",
                table: "recipe_ingredient");

            migrationBuilder.RenameTable(
                name: "recipe_ingredient",
                newName: "recipe_ingredient_rel");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_ingredient_recipe_key",
                table: "recipe_ingredient_rel",
                newName: "IX_recipe_ingredient_rel_recipe_key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel",
                column: "ingredient_key");

            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    ingredient_key = table.Column<Guid>(type: "uuid", nullable: false),
                    ingredient_name = table.Column<string>(type: "text", nullable: false),
                    ingredient_note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.ingredient_key);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_ingredient_rel_recipe_recipe_key",
                table: "recipe_ingredient_rel",
                column: "recipe_key",
                principalTable: "recipe",
                principalColumn: "recipe_key",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_ingredient_rel_recipe_recipe_key",
                table: "recipe_ingredient_rel");

            migrationBuilder.DropTable(
                name: "ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel");

            migrationBuilder.RenameTable(
                name: "recipe_ingredient_rel",
                newName: "recipe_ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_ingredient_rel_recipe_key",
                table: "recipe_ingredient",
                newName: "IX_recipe_ingredient_recipe_key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_ingredient",
                table: "recipe_ingredient",
                column: "ingredient_key");

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_ingredient_recipe_recipe_key",
                table: "recipe_ingredient",
                column: "recipe_key",
                principalTable: "recipe",
                principalColumn: "recipe_key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
