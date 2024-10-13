using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tickets.DLL.Migrations
{
    /// <inheritdoc />
    public partial class updatePricePerTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TicketPrice",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Events");
        }
    }
}
