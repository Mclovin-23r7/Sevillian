using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sevillian.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabitCompletion",
                columns: table => new
                {
                    HabitId = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformanceDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitCompletion", x => new { x.HabitId, x.PerformanceDate });
                    table.ForeignKey(
                        name: "FK_HabitCompletion_Habit_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habit_Id",
                table: "Habit",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitCompletion");

            migrationBuilder.DropTable(
                name: "Habit");
        }
    }
}
