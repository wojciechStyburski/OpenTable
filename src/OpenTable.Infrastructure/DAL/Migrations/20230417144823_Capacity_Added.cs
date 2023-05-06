using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTable.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Capacity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WeeklyOpenTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "WeeklyOpenTables",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "WeeklyOpenTables");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WeeklyOpenTables",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
