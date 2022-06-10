using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class SchedulePlannedPortions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "planned_recipe_multiplier",
                table: "schedule",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "planned_recipe_multiplier",
                table: "schedule");
        }
    }
}
