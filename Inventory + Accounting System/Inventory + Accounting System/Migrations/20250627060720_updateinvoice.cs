using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory___Accounting_System.Migrations
{
    /// <inheritdoc />
    public partial class updateinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseInvoiceId",
                table: "LedgerEntries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_AccountId",
                table: "Vendors",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerEntries_PurchaseInvoiceId",
                table: "LedgerEntries",
                column: "PurchaseInvoiceId",
                unique: true,
                filter: "[PurchaseInvoiceId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerEntries_PurchaseInvoices_PurchaseInvoiceId",
                table: "LedgerEntries",
                column: "PurchaseInvoiceId",
                principalTable: "PurchaseInvoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Accounts_AccountId",
                table: "Vendors",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerEntries_PurchaseInvoices_PurchaseInvoiceId",
                table: "LedgerEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Accounts_AccountId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_AccountId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_LedgerEntries_PurchaseInvoiceId",
                table: "LedgerEntries");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PurchaseInvoiceId",
                table: "LedgerEntries");
        }
    }
}
