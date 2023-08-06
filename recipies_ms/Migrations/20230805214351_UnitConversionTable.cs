using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class UnitConversionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "unit_conversion_rel",
                columns: table => new
                {
                    si_unit_source = table.Column<int>(type: "integer", nullable: false),
                    si_unit_target = table.Column<int>(type: "integer", nullable: false),
                    amount_source = table.Column<float>(type: "real", nullable: false),
                    amount_target = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_conversion_rel", x => new { x.si_unit_source, x.si_unit_target });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "unit_conversion_rel");
        }
    }
}
