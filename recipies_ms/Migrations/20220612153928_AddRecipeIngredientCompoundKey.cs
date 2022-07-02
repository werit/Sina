using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class AddRecipeIngredientCompoundKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel",
                columns: new[] { "ingredient_key", "recipe_key" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel",
                column: "ingredient_key");
        }
    }
}
