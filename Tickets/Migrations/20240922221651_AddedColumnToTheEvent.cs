using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tickets.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnToTheEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
           name: "Approved",
           table: "Events",
           type: "bit",
           nullable: false, // Set to false if this column should not allow nulls
           defaultValue: false // Specify a default value for existing rows
       );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
         name: "Approved",
         table: "Event"
     );
        }
    }
}
