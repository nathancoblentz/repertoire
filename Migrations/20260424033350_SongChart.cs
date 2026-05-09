using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoblentzContext.Migrations
{
    /// <inheritdoc />
    public partial class SongChart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SongChart",
                table: "Song",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 1,
                column: "SongChart",
                value: null);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 2,
                column: "SongChart",
                value: null);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 3,
                column: "SongChart",
                value: null);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 4,
                column: "SongChart",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongChart",
                table: "Song");
        }
    }
}
