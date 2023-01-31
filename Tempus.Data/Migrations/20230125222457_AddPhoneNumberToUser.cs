using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_UserId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "ProfilePhotos");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_UserId",
                table: "ProfilePhotos",
                newName: "IX_ProfilePhotos_UserId");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilePhotos",
                table: "ProfilePhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_Users_UserId",
                table: "ProfilePhotos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_Users_UserId",
                table: "ProfilePhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilePhotos",
                table: "ProfilePhotos");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "ProfilePhotos",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_ProfilePhotos_UserId",
                table: "Photos",
                newName: "IX_Photos_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_UserId",
                table: "Photos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
