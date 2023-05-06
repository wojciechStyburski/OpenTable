using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenTable.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Holiday_Reservation_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Reservations");
        }
    }
}
