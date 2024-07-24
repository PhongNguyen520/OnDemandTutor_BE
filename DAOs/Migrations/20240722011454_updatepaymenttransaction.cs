using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class updatepaymenttransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "PaymentTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_WalletId",
                table: "PaymentTransaction",
                newName: "IX_PaymentTransaction_WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_PaymentDestinationId",
                table: "PaymentTransaction",
                newName: "IX_PaymentTransaction_PaymentDestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PaymentTransaction",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransaction_WalletId",
                table: "Payment",
                newName: "IX_Payment_WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransaction_PaymentDestinationId",
                table: "Payment",
                newName: "IX_Payment_PaymentDestinationId");
        }
    }
}
