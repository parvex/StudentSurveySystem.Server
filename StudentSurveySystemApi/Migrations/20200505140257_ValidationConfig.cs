using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class ValidationConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Questions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ValidationConfig",
                table: "Questions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ValidationConfig",
                table: "Questions");
        }
    }
}
