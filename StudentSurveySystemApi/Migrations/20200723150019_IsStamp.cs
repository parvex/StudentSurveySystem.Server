using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class IsStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseParticipants_CourseId",
                table: "CourseParticipants");

            migrationBuilder.DropIndex(
                name: "IX_CourseLecturers_CourseId",
                table: "CourseLecturers");

            migrationBuilder.AddColumn<bool>(
                name: "IsStamp",
                table: "SurveyResponses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CourseParticipants_CourseId_ParticipantId",
                table: "CourseParticipants",
                columns: new[] { "CourseId", "ParticipantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturers_CourseId_LecturerId",
                table: "CourseLecturers",
                columns: new[] { "CourseId", "LecturerId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseParticipants_CourseId_ParticipantId",
                table: "CourseParticipants");

            migrationBuilder.DropIndex(
                name: "IX_CourseLecturers_CourseId_LecturerId",
                table: "CourseLecturers");

            migrationBuilder.DropColumn(
                name: "IsStamp",
                table: "SurveyResponses");

            migrationBuilder.CreateIndex(
                name: "IX_CourseParticipants_CourseId",
                table: "CourseParticipants",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturers_CourseId",
                table: "CourseLecturers",
                column: "CourseId");
        }
    }
}
