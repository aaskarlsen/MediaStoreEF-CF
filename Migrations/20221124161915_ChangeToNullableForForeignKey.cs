using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaStoreEF_CF.Migrations
{
    public partial class ChangeToNullableForForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movies_franchises_FranchiseId",
                table: "movies");

            migrationBuilder.AlterColumn<int>(
                name: "FranchiseId",
                table: "movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_movies_franchises_FranchiseId",
                table: "movies",
                column: "FranchiseId",
                principalTable: "franchises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movies_franchises_FranchiseId",
                table: "movies");

            migrationBuilder.AlterColumn<int>(
                name: "FranchiseId",
                table: "movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_movies_franchises_FranchiseId",
                table: "movies",
                column: "FranchiseId",
                principalTable: "franchises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
