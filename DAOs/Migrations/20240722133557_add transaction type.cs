using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class addtransactiontype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PaymentTransaction",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "PaymentTransaction");
        }
    }
}
