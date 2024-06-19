using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class aumentandoConcluded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Concluded",
                table: "Assignment",
                type: "VARCHAR(5)",
                nullable: false,
                defaultValue: "False",
                oldClrType: typeof(sbyte),
                oldType: "TINYINT",
                oldDefaultValue: (sbyte)0)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "Concluded",
                table: "Assignment",
                type: "TINYINT",
                nullable: false,
                defaultValue: (sbyte)0,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5)",
                oldDefaultValue: "False")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
