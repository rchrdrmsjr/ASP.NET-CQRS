using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todoApi.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Todos",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IsComplete",
                table: "Todos",
                newName: "isComplete");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Todos",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Todos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "isComplete",
                table: "Todos",
                newName: "IsComplete");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Todos",
                newName: "Id");
        }
    }
}
