using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoblentzContext.Migrations
{
    /// <inheritdoc />
    public partial class AddYouTubeUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_AspNetUsers_UserId",
                table: "Song");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Song",
                type: "nvarchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)");

            migrationBuilder.AddColumn<string>(
                name: "YouTubeUrl",
                table: "Song",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 1,
                column: "YouTubeUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 2,
                column: "YouTubeUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 3,
                column: "YouTubeUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "SongId",
                keyValue: 4,
                column: "YouTubeUrl",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_AspNetUsers_UserId",
                table: "Song",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_AspNetUsers_UserId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "YouTubeUrl",
                table: "Song");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Song",
                type: "nvarchar(128)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_AspNetUsers_UserId",
                table: "Song",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
