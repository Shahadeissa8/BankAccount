using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccount.Migrations
{
    /// <inheritdoc />
    public partial class FKforgotten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MyTransactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MyTransactions_UserId",
                table: "MyTransactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyTransactions_AspNetUsers_UserId",
                table: "MyTransactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyTransactions_AspNetUsers_UserId",
                table: "MyTransactions");

            migrationBuilder.DropIndex(
                name: "IX_MyTransactions_UserId",
                table: "MyTransactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MyTransactions");
        }
    }
}
