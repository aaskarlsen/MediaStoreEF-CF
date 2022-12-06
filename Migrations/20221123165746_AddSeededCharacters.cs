using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaStoreEF_CF.Migrations
{
    public partial class AddSeededCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "characters",
                columns: new[] { "Id", "Alias", "CharacterName", "Gender", "LinkToPicture" },
                values: new object[] { 1, "N/A", "Liv Taylor", "F", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Liv_Tyler_%2829566238128%29_%28cropped%29.jpg/220px-Liv_Tyler_%2829566238128%29_%28cropped%29.jpg" });

            migrationBuilder.InsertData(
                table: "characters",
                columns: new[] { "Id", "Alias", "CharacterName", "Gender", "LinkToPicture" },
                values: new object[] { 2, "N/A", "Viggo Mortensen", "M", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Viggo_Mortensen_B_%282020%29.jpg/220px-Viggo_Mortensen_B_%282020%29.jpg" });

            migrationBuilder.InsertData(
                table: "characters",
                columns: new[] { "Id", "Alias", "CharacterName", "Gender", "LinkToPicture" },
                values: new object[] { 3, "N/A", "Martin John Christopher Freeman", "M", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7f/Martin_Freeman-5341.jpg/220px-Martin_Freeman-5341.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
