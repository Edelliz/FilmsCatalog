using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsCatalog.Migrations
{
    public partial class MovieDirectorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MovieDirectorId",
                table: "Movie",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MovieDirectors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDirectors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_MovieDirectorId",
                table: "Movie",
                column: "MovieDirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_MovieDirectors_MovieDirectorId",
                table: "Movie",
                column: "MovieDirectorId",
                principalTable: "MovieDirectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_MovieDirectors_MovieDirectorId",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "MovieDirectors");

            migrationBuilder.DropIndex(
                name: "IX_Movie_MovieDirectorId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "MovieDirectorId",
                table: "Movie");
        }
    }
}
