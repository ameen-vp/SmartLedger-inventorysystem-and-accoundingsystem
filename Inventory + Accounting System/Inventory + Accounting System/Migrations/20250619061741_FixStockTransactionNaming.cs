using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory___Accounting_System.Migrations
{
    /// <inheritdoc />
    public partial class FixStockTransactionNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stockTransactions_Stocks_StockId",
                table: "stockTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_stockTransactions_Stocks_StockId",
                table: "stockTransactions",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stockTransactions_Stocks_StockId",
                table: "stockTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_stockTransactions_Stocks_StockId",
                table: "stockTransactions",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }
    }
}
