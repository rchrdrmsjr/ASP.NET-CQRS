using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

public partial class CreateTodoTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Drop the table if it exists (be careful in production!)
        migrationBuilder.Sql("DROP TABLE IF EXISTS \"Todos\" CASCADE;");

        // Create the table with proper lowercase column names
        migrationBuilder.CreateTable(
            name: "Todos",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: true),
                isComplete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Todos", x => x.id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Todos");
    }
}