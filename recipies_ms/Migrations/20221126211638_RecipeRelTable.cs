using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class RecipeRelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel");

            migrationBuilder.DropIndex(
                name: "IX_recipe_ingredient_rel_recipe_key",
                table: "recipe_ingredient_rel");

            migrationBuilder.DropColumn(
                name: "ingredient",
                table: "recipe_ingredient_rel");

            migrationBuilder.RenameColumn(
                name: "ingredient_note",
                table: "recipe_ingredient_rel",
                newName: "ingredient_recipe_note");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel",
                columns: new[] { "recipe_key", "ingredient_key" });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredient_rel_ingredient_key",
                table: "recipe_ingredient_rel",
                column: "ingredient_key");

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_ingredient_rel_ingredient_ingredient_key",
                table: "recipe_ingredient_rel",
                column: "ingredient_key",
                principalTable: "ingredient",
                principalColumn: "ingredient_key",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_ingredient_rel_ingredient_ingredient_key",
                table: "recipe_ingredient_rel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel");

            migrationBuilder.DropIndex(
                name: "IX_recipe_ingredient_rel_ingredient_key",
                table: "recipe_ingredient_rel");

            migrationBuilder.RenameColumn(
                name: "ingredient_recipe_note",
                table: "recipe_ingredient_rel",
                newName: "ingredient_note");

            migrationBuilder.AddColumn<string>(
                name: "ingredient",
                table: "recipe_ingredient_rel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_ingredient_rel",
                table: "recipe_ingredient_rel",
                columns: new[] { "ingredient_key", "recipe_key" });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredient_rel_recipe_key",
                table: "recipe_ingredient_rel",
                column: "recipe_key");
        }
    }
}
