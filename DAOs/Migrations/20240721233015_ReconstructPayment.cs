using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class ReconstructPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryTransaction");

            migrationBuilder.DropColumn(
                name: "ResponseCode",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "TranStatus",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "TxnRef",
                table: "PaymentTransaction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponseCode",
                table: "PaymentTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranStatus",
                table: "PaymentTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TxnRef",
                table: "PaymentTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoryTransaction",
                columns: table => new
                {
                    HistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WalletId = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: true),
                    BackTranNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HistoryTransaction__77C70FC2A16C5PHUC", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FKTutor3875333",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "WalletID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTransaction_WalletId",
                table: "HistoryTransaction",
                column: "WalletId");
        }
    }
}
