using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPlace.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperationType",
                table: "BalanceLog_1",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationType",
                table: "BalanceLog_1");
        }
    }
}
