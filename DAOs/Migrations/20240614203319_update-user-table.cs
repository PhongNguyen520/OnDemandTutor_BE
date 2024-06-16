using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class updateusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldUnicode: false,
                oldMaxLength: 256);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldUnicode: false,
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
