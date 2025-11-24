using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMonetaryFundFromBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_MonetaryFunds_MonetaryFundId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_MonetaryFundId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_Year_Month_ExpenseTypeId_MonetaryFundId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "MonetaryFundId",
                table: "Budgets");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_Year_Month_ExpenseTypeId",
                table: "Budgets",
                columns: new[] { "Year", "Month", "ExpenseTypeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Budgets_Year_Month_ExpenseTypeId",
                table: "Budgets");

            migrationBuilder.AddColumn<int>(
                name: "MonetaryFundId",
                table: "Budgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_MonetaryFundId",
                table: "Budgets",
                column: "MonetaryFundId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_Year_Month_ExpenseTypeId_MonetaryFundId",
                table: "Budgets",
                columns: new[] { "Year", "Month", "ExpenseTypeId", "MonetaryFundId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_MonetaryFunds_MonetaryFundId",
                table: "Budgets",
                column: "MonetaryFundId",
                principalTable: "MonetaryFunds",
                principalColumn: "FundId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
