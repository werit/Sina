using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class UnitConversionDataExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "unit",
                column: "si_unit",
                values: new object[]
                {
                    "Oz",
                    "Pound",
                    "FlOz"
                });

            migrationBuilder.InsertData(
                table: "unit_conversion_rel",
                columns: new[] { "si_unit_source", "si_unit_target", "amount_source", "amount_target" },
                values: new object[,]
                {
                    { "Cup", "FlOz", 1f, 8.8f },
                    { "Cup", "Oz", 0.1134f, 1f },
                    { "Oz", "Gram", 1f, 28.35f },
                    { "Oz", "Pound", 1f, 0.0625f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "Oz");

            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "Pound");

            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "FlOz");

            migrationBuilder.DeleteData(
                table: "unit_conversion_rel",
                keyColumns: new[] { "si_unit_source", "si_unit_target" },
                keyValues: new object[] { "Cup", "Oz" });

            migrationBuilder.DeleteData(
                table: "unit_conversion_rel",
                keyColumns: new[] { "si_unit_source", "si_unit_target" },
                keyValues: new object[] { "Cup", "FlOz" });

            migrationBuilder.DeleteData(
                table: "unit_conversion_rel",
                keyColumns: new[] { "si_unit_source", "si_unit_target" },
                keyValues: new object[] { "Oz", "Gram" });

            migrationBuilder.DeleteData(
                table: "unit_conversion_rel",
                keyColumns: new[] { "si_unit_source", "si_unit_target" },
                keyValues: new object[] { "Oz", "Pound" });
        }
    }
}
