using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectName",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "DayPerWeek",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "HourPerDay",
                table: "Class");

            migrationBuilder.AddColumn<DateTime>(
                name: "DayEnd",
                table: "FindTutorForm",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeek",
                table: "FindTutorForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DayStart",
                table: "FindTutorForm",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimeEnd",
                table: "FindTutorForm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeStart",
                table: "FindTutorForm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DayEnd",
                table: "Class",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DayStart",
                table: "Class",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ClassCalender",
                columns: table => new
                {
                    CalenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DayOfWeek = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeStart = table.Column<int>(type: "int", nullable: false),
                    TimeEnd = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClassCalender__77C70FC2A16C50DB1", x => x.CalenderId);
                    table.ForeignKey(
                        name: "FKClassCal3875973",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassID");
                });

            migrationBuilder.CreateTable(
                name: "RequestTutorForm",
                columns: table => new
                {
                    FormId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStart = table.Column<int>(type: "int", nullable: false),
                    TimeEnd = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    SubjectId = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    TutorId = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RequestTutorForm__77C70FC2A16C50DB2", x => x.FormId);
                    table.ForeignKey(
                        name: "FKRequestTu3875945",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectID");
                    table.ForeignKey(
                        name: "FKRequestTu3875974",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "TutorID");
                    table.ForeignKey(
                        name: "FKRequestTu3875B45",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassCalender_ClassId",
                table: "ClassCalender",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTutorForm_StudentId",
                table: "RequestTutorForm",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTutorForm_SubjectId",
                table: "RequestTutorForm",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTutorForm_TutorId",
                table: "RequestTutorForm",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassCalender");

            migrationBuilder.DropTable(
                name: "RequestTutorForm");

            migrationBuilder.DropColumn(
                name: "DayEnd",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "DayStart",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "TimeStart",
                table: "FindTutorForm");

            migrationBuilder.DropColumn(
                name: "DayEnd",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "DayStart",
                table: "Class");

            migrationBuilder.AddColumn<string>(
                name: "SubjectName",
                table: "FindTutorForm",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DayPerWeek",
                table: "Class",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "HourPerDay",
                table: "Class",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
