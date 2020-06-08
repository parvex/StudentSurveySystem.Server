using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anonymnous",
                table: "Surveys");

            migrationBuilder.AddColumn<bool>(
                name: "Anonymous",
                table: "Surveys",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anonymous",
                table: "Surveys");

            migrationBuilder.AddColumn<bool>(
                name: "Anonymnous",
                table: "Surveys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
