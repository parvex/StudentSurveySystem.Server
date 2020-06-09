using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_SurveyResponses_SurveyResponseId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semester_SemesterId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semester",
                table: "Semester");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "Semester",
                newName: "Semesters");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_SurveyResponseId",
                table: "Answers",
                newName: "IX_Answers_SurveyResponseId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_SurveyResponses_SurveyResponseId",
                table: "Answers",
                column: "SurveyResponseId",
                principalTable: "SurveyResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semesters_SemesterId",
                table: "Courses",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_SurveyResponses_SurveyResponseId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semesters_SemesterId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "Semesters",
                newName: "Semester");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_SurveyResponseId",
                table: "Answer",
                newName: "IX_Answer_SurveyResponseId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semester",
                table: "Semester",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_SurveyResponses_SurveyResponseId",
                table: "Answer",
                column: "SurveyResponseId",
                principalTable: "SurveyResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semester_SemesterId",
                table: "Courses",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
