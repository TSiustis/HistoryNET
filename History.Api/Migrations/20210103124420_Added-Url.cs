using Microsoft.EntityFrameworkCore.Migrations;

namespace History.Api.Migrations
{
    public partial class AddedUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Death",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Birth",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Death");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Birth");
        }
    }
}
