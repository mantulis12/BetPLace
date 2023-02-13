using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlace.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BetEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Team2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    coef1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    coef0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    coef2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Team1Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Team2Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetEvent", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetEvent");
        }
    }
}
