using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotatnikUzytkownikow.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AdditionalAttributes",
                newName: "AttributeName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AttributeName",
                table: "AdditionalAttributes",
                newName: "Name");
        }
    }
}
