using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class EndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Surveys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Surveys");
        }
    }
}
