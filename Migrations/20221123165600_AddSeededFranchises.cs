using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaStoreEF_CF.Migrations
{
    public partial class AddSeededFranchises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "franchises",
                columns: new[] { "Id", "Description", "FranchiseTitle" },
                values: new object[] { 1, "Fantasy adventure film", "Lord of the Rings" });

            migrationBuilder.InsertData(
                table: "franchises",
                columns: new[] { "Id", "Description", "FranchiseTitle" },
                values: new object[] { 2, "An American media franchise and shared universe centered on a series of superhero films produced by Marvel Studios.", "The Marvel Cinematic Universe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "franchises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "franchises",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
