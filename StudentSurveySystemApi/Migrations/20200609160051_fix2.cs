using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class fix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLecturer_Courses_CourseId",
                table: "CourseLecturer");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseLecturer_Users_LecturerId",
                table: "CourseLecturer");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseParticipant_Courses_CourseId",
                table: "CourseParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseParticipant_Users_ParticipantId",
                table: "CourseParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseParticipant",
                table: "CourseParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLecturer",
                table: "CourseLecturer");

            migrationBuilder.RenameTable(
                name: "CourseParticipant",
                newName: "CourseParticipants");

            migrationBuilder.RenameTable(
                name: "CourseLecturer",
                newName: "CourseLecturers");

            migrationBuilder.RenameIndex(
                name: "IX_CourseParticipant_ParticipantId",
                table: "CourseParticipants",
                newName: "IX_CourseParticipants_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseParticipant_CourseId",
                table: "CourseParticipants",
                newName: "IX_CourseParticipants_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLecturer_LecturerId",
                table: "CourseLecturers",
                newName: "IX_CourseLecturers_LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLecturer_CourseId",
                table: "CourseLecturers",
                newName: "IX_CourseLecturers_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseParticipants",
                table: "CourseParticipants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLecturers",
                table: "CourseLecturers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLecturers_Courses_CourseId",
                table: "CourseLecturers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLecturers_Users_LecturerId",
                table: "CourseLecturers",
                column: "LecturerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseParticipants_Courses_CourseId",
                table: "CourseParticipants",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseParticipants_Users_ParticipantId",
                table: "CourseParticipants",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLecturers_Courses_CourseId",
                table: "CourseLecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseLecturers_Users_LecturerId",
                table: "CourseLecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseParticipants_Courses_CourseId",
                table: "CourseParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseParticipants_Users_ParticipantId",
                table: "CourseParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseParticipants",
                table: "CourseParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLecturers",
                table: "CourseLecturers");

            migrationBuilder.RenameTable(
                name: "CourseParticipants",
                newName: "CourseParticipant");

            migrationBuilder.RenameTable(
                name: "CourseLecturers",
                newName: "CourseLecturer");

            migrationBuilder.RenameIndex(
                name: "IX_CourseParticipants_ParticipantId",
                table: "CourseParticipant",
                newName: "IX_CourseParticipant_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseParticipants_CourseId",
                table: "CourseParticipant",
                newName: "IX_CourseParticipant_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLecturers_LecturerId",
                table: "CourseLecturer",
                newName: "IX_CourseLecturer_LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLecturers_CourseId",
                table: "CourseLecturer",
                newName: "IX_CourseLecturer_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseParticipant",
                table: "CourseParticipant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLecturer",
                table: "CourseLecturer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLecturer_Courses_CourseId",
                table: "CourseLecturer",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLecturer_Users_LecturerId",
                table: "CourseLecturer",
                column: "LecturerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseParticipant_Courses_CourseId",
                table: "CourseParticipant",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseParticipant_Users_ParticipantId",
                table: "CourseParticipant",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
