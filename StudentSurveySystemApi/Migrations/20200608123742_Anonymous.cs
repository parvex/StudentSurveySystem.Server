using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class Anonymous : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Anonymnous",
                table: "Surveys",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anonymnous",
                table: "Surveys");
        }
    }
}
