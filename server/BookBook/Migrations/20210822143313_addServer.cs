using Microsoft.EntityFrameworkCore.Migrations;

namespace BookBook.Migrations
{
    public partial class addServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Seat",
                table: "Tickets",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seat",
                table: "Tickets");
        }
    }
}
