using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlace.Migrations
{
    /// <inheritdoc />
    public partial class AddEventResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WiningTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetEventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventResult_BetEvent_BetEventId",
                        column: x => x.BetEventId,
                        principalTable: "BetEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventResult_BetEventId",
                table: "EventResult",
                column: "BetEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventResult");
        }
    }
}
