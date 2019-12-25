using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Galaxy.Planets.Presentation.Migrations
{
    public partial class ExplorationDateToNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PhaseFinishTime",
                table: "Explorations",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PhaseFinishTime",
                table: "Explorations",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
