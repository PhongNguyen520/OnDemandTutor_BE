using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOs.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Merchant_MerchantId",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.DropIndex(
                name: "IX_Payment_MerchantId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "Payment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MerchantId",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MerchantIpnUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantReturnUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantWebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Merchant__C050D887C401235FC", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_MerchantId",
                table: "Payment",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Merchant_MerchantId",
                table: "Payment",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "Id");
        }
    }
}
