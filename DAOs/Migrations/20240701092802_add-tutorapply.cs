using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class addtutorapply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorApply",
                columns: table => new
                {
                    TutorId = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    FormId = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    DayApply = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApprove = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TutorAp___9B67D374BBB4AFDSS", x => new { x.FormId, x.TutorId });
                    table.ForeignKey(
                        name: "FKTutorApply31899",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "TutorID");
                    table.ForeignKey(
                        name: "FKTutorApply6750344",
                        column: x => x.FormId,
                        principalTable: "FindTutorForm",
                        principalColumn: "FormID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorApply_TutorId",
                table: "TutorApply",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorApply");
        }
    }
}
