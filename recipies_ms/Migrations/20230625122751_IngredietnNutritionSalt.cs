using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class IngredietnNutritionSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "kj_energy_content",
                table: "ingredient_nutrition",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "salt_content_percentage",
                table: "ingredient_nutrition",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kj_energy_content",
                table: "ingredient_nutrition");

            migrationBuilder.DropColumn(
                name: "salt_content_percentage",
                table: "ingredient_nutrition");
        }
    }
}
