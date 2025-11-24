using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseDetailBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "ExpenseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BudgetId1",
                table: "ExpenseDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDetails_BudgetId",
                table: "ExpenseDetails",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDetails_BudgetId1",
                table: "ExpenseDetails",
                column: "BudgetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseDetails_Budgets_BudgetId",
                table: "ExpenseDetails",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseDetails_Budgets_BudgetId1",
                table: "ExpenseDetails",
                column: "BudgetId1",
                principalTable: "Budgets",
                principalColumn: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseDetails_Budgets_BudgetId",
                table: "ExpenseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseDetails_Budgets_BudgetId1",
                table: "ExpenseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseDetails_BudgetId",
                table: "ExpenseDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseDetails_BudgetId1",
                table: "ExpenseDetails");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "ExpenseDetails");

            migrationBuilder.DropColumn(
                name: "BudgetId1",
                table: "ExpenseDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
