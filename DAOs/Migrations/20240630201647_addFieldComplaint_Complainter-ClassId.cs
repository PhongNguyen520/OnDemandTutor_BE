using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class addFieldComplaint_ComplainterClassId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassId",
                table: "Complaint",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complainter",
                table: "Complaint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_ClassId",
                table: "Complaint",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FKComplaint1966098",
                table: "Complaint",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "ClassID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKComplaint1966098",
                table: "Complaint");

            migrationBuilder.DropIndex(
                name: "IX_Complaint_ClassId",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "Complainter",
                table: "Complaint");
        }
    }
}
