using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.Data.Migrations
{
    /// <inheritdoc />
    public partial class GroupsTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupCategories_Group_GroupId",
                table: "GroupCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPhotos_Group_GroupId",
                table: "GroupPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Group_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupCategories_Groups_GroupId",
                table: "GroupCategories",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPhotos_Groups_GroupId",
                table: "GroupPhotos",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Groups_GroupId",
                table: "GroupUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupCategories_Groups_GroupId",
                table: "GroupCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPhotos_Groups_GroupId",
                table: "GroupPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Groups_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupCategories_Group_GroupId",
                table: "GroupCategories",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPhotos_Group_GroupId",
                table: "GroupPhotos",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Group_GroupId",
                table: "GroupUsers",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
