using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habit.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedDays",
                table: "Habit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetDays",
                table: "Habit",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedDays",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "TargetDays",
                table: "Habit");
        }
    }
}
