using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class RestructorPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "RequiredAmount",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "TxnRef",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payment",
                newName: "IsValid");

            migrationBuilder.RenameColumn(
                name: "ExpireDate",
                table: "Payment",
                newName: "TranDate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "Payment",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "TranDate",
                table: "Payment",
                newName: "ExpireDate");

            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "Payment",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "RequiredAmount",
                table: "Payment",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TxnRef",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PaymentTransaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: true),
                    BankTranNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentTransaction__CB1927A1291BF343P", x => x.Id);
                    table.ForeignKey(
                        name: "FKPaymentTransaction3171253",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_PaymentId",
                table: "PaymentTransaction",
                column: "PaymentId");
        }
    }
}
