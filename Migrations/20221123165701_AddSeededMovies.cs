using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaStoreEF_CF.Migrations
{
    public partial class AddSeededMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "LinkToMoviePicture", "MovieTitle", "ReleaseYear", "Trailer" },
                values: new object[,]
                {
                    { 1, "Peter Jackson", 1, "Fantasy", "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Lord_of_the_Rings_The_Fellowship_of_the_Ring_%282001%29.jpg", "The Fellowship of the Ring", 2001, "https://www.youtube.com/watch?v=_e8QGuG50ro" },
                    { 2, "Peter Jackson", 1, "Fantasy", "https://upload.wikimedia.org/wikipedia/en/d/d0/Lord_of_the_Rings_-_The_Two_Towers_%282002%29.jpg", "The Two Towers", 2002, "https://www.youtube.com/watch?v=hYcw5ksV8YQ" },
                    { 3, "Peter Jackson", 1, "Fantasy", "https://upload.wikimedia.org/wikipedia/en/b/be/The_Lord_of_the_Rings_-_The_Return_of_the_King_%282003%29.jpg", "The Return of the King", 2003, "https://www.youtube.com/watch?v=r5X-hFf6Bwo" },
                    { 4, "Peter Jackson", 1, "Fantasy", "https://upload.wikimedia.org/wikipedia/en/thumb/a/a9/The_Hobbit_trilogy_dvd_cover.jpg/220px-The_Hobbit_trilogy_dvd_cover.jpg", "The Hobbit: An Unexpected Journey", 2012, "https://www.youtube.com/watch?v=SDnYMbYB-nU" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
