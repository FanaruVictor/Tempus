using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRegistrationDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Registrations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Registrations");
        }
    }
}
