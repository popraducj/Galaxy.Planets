using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Galaxy.Planets.Presentation.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Explorations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    PlanetId = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PhaseFinishTime = table.Column<DateTime>(nullable: false),
                    RobotsReports = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Explorations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    ImagePath = table.Column<string>(type: "varchar(2048)", nullable: true),
                    Description = table.Column<string>(type: "varchar(2500)", nullable: true),
                    Units = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Explorations");

            migrationBuilder.DropTable(
                name: "Planets");
        }
    }
}
