using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class UnitConversionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "unit_conversion_rel",
                columns: new[] { "si_unit_source", "si_unit_target", "amount_source", "amount_target" },
                values: new object[,]
                {
                    { "Cup", "Gram", 1f, 250f },
                    { "Cup", "TeaSpoon", 1f, 50f },
                    { "Cup", "TableSpoon", 1f, 16.67f },
                    { "Cup", "Milliliter", 1f, 250f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "unit_conversion_rel",
                keyColumns: new[] { "si_unit_source", "si_unit_target" },
                keyValues: new object[] { "Cup", "Milliliter" });

            migrationBuilder.DeleteData(
                table: "unit_conversion_rel",
                keyColumns: new[] { "si_unit_source", "si_unit_target" },
                keyValues: new object[] { "Cup", "TeaSpoon" });

            migrationBuilder.DeleteData(
                table: "unit_conversion_rel",
                keyColumns: new[] { "si_unit_source", "si_unit_target" },
                keyValues: new object[] { "Cup", "TableSpoon" });

            migrationBuilder.DeleteData(
                table: "unit_conversion_rel",
                keyColumns: new[] { "si_unit_source", "si_unit_target" },
                keyValues: new object[] { "Cup", "Gram" });
        }
    }
}
