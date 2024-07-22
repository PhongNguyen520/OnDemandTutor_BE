using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class TableHistoryTutorApply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryTutorApply",
                columns: table => new
                {
                    HistoryTutorApplyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateInterView = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    TutorId = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HistoryTutorApplyId__32C52A79D012377FD", x => x.HistoryTutorApplyId);
                    table.ForeignKey(
                        name: "FKStudent7222242",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "TutorID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTutorApply_TutorId",
                table: "HistoryTutorApply",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryTutorApply");
        }
    }
}
