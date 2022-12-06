using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaStoreEF_CF.Migrations
{
    public partial class AddSeededManyToManyRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 3, 4 });
        }
    }
}
