using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModel.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAtTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "StockHoldings");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "StockSymbol",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "StockHoldings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "StockSymbol",
                table: "StockHoldings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockSymbol",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "StockSymbol",
                table: "StockHoldings");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transactions",
                newName: "TransactionId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAtTime",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "StockHoldings",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "StockHoldings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
