using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixBudgetRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseDetails_Budgets_BudgetId1",
                table: "ExpenseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseDetails_ExpenseTypes_ExpenseTypeId",
                table: "ExpenseDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseDetails_BudgetId1",
                table: "ExpenseDetails");

            migrationBuilder.DropColumn(
                name: "BudgetId1",
                table: "ExpenseDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseDetails_ExpenseTypes_ExpenseTypeId",
                table: "ExpenseDetails",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "ExpenseTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseDetails_ExpenseTypes_ExpenseTypeId",
                table: "ExpenseDetails");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId1",
                table: "ExpenseDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDetails_BudgetId1",
                table: "ExpenseDetails",
                column: "BudgetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseDetails_Budgets_BudgetId1",
                table: "ExpenseDetails",
                column: "BudgetId1",
                principalTable: "Budgets",
                principalColumn: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseDetails_ExpenseTypes_ExpenseTypeId",
                table: "ExpenseDetails",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "ExpenseTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
