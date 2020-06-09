using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class CoursesAndSemesters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_LeaderId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LeaderId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SemesterPart",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseLecturer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LecturerId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLecturer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseLecturer_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseLecturer_Users_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseParticipant_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseParticipant_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SemesterId",
                table: "Courses",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturer_CourseId",
                table: "CourseLecturer",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLecturer_LecturerId",
                table: "CourseLecturer",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseParticipant_CourseId",
                table: "CourseParticipant",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseParticipant_ParticipantId",
                table: "CourseParticipant",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semester_SemesterId",
                table: "Courses",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semester_SemesterId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseLecturer");

            migrationBuilder.DropTable(
                name: "CourseParticipant");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SemesterId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "LeaderId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SemesterPart",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LeaderId",
                table: "Courses",
                column: "LeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_LeaderId",
                table: "Courses",
                column: "LeaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
