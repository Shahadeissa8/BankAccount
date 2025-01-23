using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccount.Migrations
{
    /// <inheritdoc />
    public partial class newamount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NewAmount",
                table: "MyTransactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewAmount",
                table: "MyTransactions");
        }
    }
}
