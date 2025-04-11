using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JediArchives.Migrations;

/// <inheritdoc />
public partial class AddHashedPasswordToUsers : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.AddColumn<string>(
            name: "HashedPassword",
            table: "Users",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropColumn(
            name: "HashedPassword",
            table: "Users");
    }
}
