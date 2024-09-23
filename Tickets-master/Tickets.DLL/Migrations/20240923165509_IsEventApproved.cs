using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tickets.DLL.Migrations
{
    /// <inheritdoc />
    public partial class IsEventApproved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Approved",
                table: "Events",
                newName: "IsRejected");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "IsRejected",
                table: "Events",
                newName: "Approved");
        }
    }
}
