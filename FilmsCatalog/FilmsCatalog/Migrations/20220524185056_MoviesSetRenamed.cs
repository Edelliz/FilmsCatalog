using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsCatalog.Migrations
{
    public partial class MoviesSetRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_AspNetUsers_AddedByUserId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_MovieDirectors_MovieDirectorId",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movies");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_MovieDirectorId",
                table: "Movies",
                newName: "IX_Movies_MovieDirectorId");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_AddedByUserId",
                table: "Movies",
                newName: "IX_Movies_AddedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_AddedByUserId",
                table: "Movies",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieDirectors_MovieDirectorId",
                table: "Movies",
                column: "MovieDirectorId",
                principalTable: "MovieDirectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_AddedByUserId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieDirectors_MovieDirectorId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movie");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_MovieDirectorId",
                table: "Movie",
                newName: "IX_Movie_MovieDirectorId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_AddedByUserId",
                table: "Movie",
                newName: "IX_Movie_AddedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_AspNetUsers_AddedByUserId",
                table: "Movie",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_MovieDirectors_MovieDirectorId",
                table: "Movie",
                column: "MovieDirectorId",
                principalTable: "MovieDirectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
