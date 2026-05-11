using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSLS.Fullstack.Api.Migrations;

[DbContext(typeof(RPSLS.Fullstack.Api.Data.AppDbContext))]
[Migration("202605101230_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ScoreEntries",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                PlayerChoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                ComputerChoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                Result = table.Column<string>(type: "TEXT", nullable: false),
                CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ScoreEntries", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ScoreEntries_CreatedAtUtc",
            table: "ScoreEntries",
            column: "CreatedAtUtc");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ScoreEntries");
    }
}
