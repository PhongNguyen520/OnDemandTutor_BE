using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKPayment3070123",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FKPaymentTransaction3271453",
                table: "PaymentTransaction");

            migrationBuilder.DropTable(
                name: "PaymentSignature");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_WalletId",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "TranAmount",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "PaymentDestination");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentLanguage",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "TranRefId",
                table: "PaymentTransaction",
                newName: "TxnRef");

            migrationBuilder.RenameColumn(
                name: "TranPayload",
                table: "PaymentTransaction",
                newName: "ResponseCode");

            migrationBuilder.RenameColumn(
                name: "TranMessage",
                table: "PaymentTransaction",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "DesShortName",
                table: "PaymentDestination",
                newName: "BankName");

            migrationBuilder.RenameColumn(
                name: "DesName",
                table: "PaymentDestination",
                newName: "BankLogo");

            migrationBuilder.RenameColumn(
                name: "DesLogo",
                table: "PaymentDestination",
                newName: "BankCode");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Payment",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "PaymentRefId",
                table: "Payment",
                newName: "TxnRef");

            migrationBuilder.RenameColumn(
                name: "PaymentLastMessage",
                table: "Payment",
                newName: "Signature");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Payment",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "PaymentCurrency",
                table: "Payment",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "PaymentContent",
                table: "Payment",
                newName: "CurrencyCode");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "PaymentTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BankTranNo",
                table: "PaymentTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                table: "PaymentTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "PaymentTransaction",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "RequiredAmount",
                table: "Payment",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "Payment",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_WalletId",
                table: "Payment",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FKPayment1158769",
                table: "Payment",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "WalletID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Merchant_MerchantId",
                table: "Payment",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKPayment1158769",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Merchant_MerchantId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_WalletId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "BankTranNo",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "TxnRef",
                table: "PaymentTransaction",
                newName: "TranRefId");

            migrationBuilder.RenameColumn(
                name: "ResponseCode",
                table: "PaymentTransaction",
                newName: "TranPayload");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PaymentTransaction",
                newName: "TranMessage");

            migrationBuilder.RenameColumn(
                name: "BankName",
                table: "PaymentDestination",
                newName: "DesShortName");

            migrationBuilder.RenameColumn(
                name: "BankLogo",
                table: "PaymentDestination",
                newName: "DesName");

            migrationBuilder.RenameColumn(
                name: "BankCode",
                table: "PaymentDestination",
                newName: "DesLogo");

            migrationBuilder.RenameColumn(
                name: "TxnRef",
                table: "Payment",
                newName: "PaymentRefId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payment",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "Signature",
                table: "Payment",
                newName: "PaymentLastMessage");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Payment",
                newName: "PaymentCurrency");

            migrationBuilder.RenameColumn(
                name: "CurrencyCode",
                table: "Payment",
                newName: "PaymentContent");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Payment",
                newName: "PaymentDate");

            migrationBuilder.AddColumn<decimal>(
                name: "TranAmount",
                table: "PaymentTransaction",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "PaymentTransaction",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "PaymentDestination",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "RequiredAmount",
                table: "Payment",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Payment",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentLanguage",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentSignature",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    SignAlgo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SignOwn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentSignature__CB1927A1291BF352P", x => x.Id);
                    table.ForeignKey(
                        name: "FKPaymentSignature3071243",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_WalletId",
                table: "PaymentTransaction",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSignature_PaymentId",
                table: "PaymentSignature",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FKPayment3070123",
                table: "Payment",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FKPaymentTransaction3271453",
                table: "PaymentTransaction",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "WalletID");
        }
    }
}
