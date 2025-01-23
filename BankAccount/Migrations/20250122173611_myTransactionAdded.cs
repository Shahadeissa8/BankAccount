using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccount.Migrations
{
    /// <inheritdoc />
    public partial class myTransactionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyTransactions",
                columns: table => new
                {
                    MyTransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Transaction = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateOfTransaction = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTransactions", x => x.MyTransactionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyTransactions");
        }
    }
}
