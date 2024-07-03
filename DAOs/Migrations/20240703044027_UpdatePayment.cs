using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKPaymentDestination3071133",
                table: "PaymentDestination");

            migrationBuilder.DropTable(
                name: "PaymentNotification");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDestination_DesParentId",
                table: "PaymentDestination");

            migrationBuilder.DropColumn(
                name: "DesParentId",
                table: "PaymentDestination");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DesParentId",
                table: "PaymentDestination",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentNotification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotiMerchantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotiPaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotiAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotiContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiResHttpCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentNotification__CB1927A0190BF352H", x => x.Id);
                    table.ForeignKey(
                        name: "FKPaymentNotification1059789",
                        column: x => x.NotiPaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FKPaymentNotification3070122",
                        column: x => x.NotiMerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDestination_DesParentId",
                table: "PaymentDestination",
                column: "DesParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentNotification_NotiMerchantId",
                table: "PaymentNotification",
                column: "NotiMerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentNotification_NotiPaymentId",
                table: "PaymentNotification",
                column: "NotiPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FKPaymentDestination3071133",
                table: "PaymentDestination",
                column: "DesParentId",
                principalTable: "PaymentDestination",
                principalColumn: "Id");
        }
    }
}
