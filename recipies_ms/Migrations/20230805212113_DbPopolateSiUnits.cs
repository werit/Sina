using Microsoft.EntityFrameworkCore.Migrations;

namespace recipies_ms.Migrations
{
    public partial class DbPopolateSiUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "unit",
                column: "si_unit",
                values: new object[]
                {
                    "Milliliter",
                    "Cup",
                    "Piece",
                    "TeaSpoon",
                    "TableSpoon",
                    "Gram"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "Milliliter");

            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "Cup");

            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "Piece");

            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "TeaSpoon");

            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "TableSpoon");

            migrationBuilder.DeleteData(
                table: "unit",
                keyColumn: "si_unit",
                keyValue: "Gram");
        }
    }
}
