using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlace.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBetEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BetEvent",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BetEvent");
        }
    }
}
