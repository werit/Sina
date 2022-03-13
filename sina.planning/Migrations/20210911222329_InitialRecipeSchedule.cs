using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sina.planning.Migrations
{
    public partial class InitialRecipeSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recipe_schedule",
                columns: table => new
                {
                    recipe_schedule_key = table.Column<Guid>(type: "uuid", nullable: false),
                    recipe_schedule_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    recipe_key = table.Column<Guid>(type: "uuid", nullable: false),
                    recipe_name = table.Column<string>(type: "text", nullable: true),
                    recipe_portions = table.Column<float>(type: "real", nullable: false),
                    valid_from = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    valid_to = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    current_flag = table.Column<char>(type: "character(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_schedule", x => x.recipe_schedule_key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipe_schedule");
        }
    }
}
