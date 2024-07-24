using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKTransactio1812602",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Transact__55433A4B61451DBED",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_WalletID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "WalletID",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "Transactions",
                newName: "TransactionId");

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "PaymentTransaction",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_WalletId",
                table: "PaymentTransaction",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FKPaymentTransaction3271453",
                table: "PaymentTransaction",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "WalletID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKPaymentTransaction3271453",
                table: "PaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_WalletId",
                table: "PaymentTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "PaymentTransaction");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Transaction",
                newName: "TransactionID");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transaction",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionID",
                table: "Transaction",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "WalletID",
                table: "Transaction",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Transact__55433A4B61451DBED",
                table: "Transaction",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_WalletID",
                table: "Transaction",
                column: "WalletID");

            migrationBuilder.AddForeignKey(
                name: "FKTransactio1812602",
                table: "Transaction",
                column: "WalletID",
                principalTable: "Wallet",
                principalColumn: "WalletID");
        }
    }
}
