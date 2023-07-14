using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class IngredientFibre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "fibre_content_percentage",
                table: "ingredient_nutrition",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fibre_content_percentage",
                table: "ingredient_nutrition");
        }
    }
}
