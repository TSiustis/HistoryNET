using Microsoft.EntityFrameworkCore.Migrations;

namespace History.Api.Migrations
{
    public partial class AddedDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Death",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Birth",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Death");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Birth");
        }
    }
}
