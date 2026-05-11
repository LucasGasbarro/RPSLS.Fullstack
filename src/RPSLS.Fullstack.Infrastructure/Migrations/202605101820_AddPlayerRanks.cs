using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSLS.Fullstack.Api.Migrations;

[DbContext(typeof(RPSLS.Fullstack.Api.Data.AppDbContext))]
[Migration("202605101820_AddPlayerRanks")]
public partial class AddPlayerRanks : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PlayerRanks",
            columns: table => new
            {
                NormalizedName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                DisplayName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Points = table.Column<int>(type: "INTEGER", nullable: false),
                UpdatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PlayerRanks", x => x.NormalizedName);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PlayerRanks_Points",
            table: "PlayerRanks",
            column: "Points");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PlayerRanks");
    }
}
