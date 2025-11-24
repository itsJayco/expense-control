using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMonetaryFundToBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_MonetaryFunds_MonetaryFundId",
                table: "Budgets",
                column: "MonetaryFundId",
                principalTable: "MonetaryFunds",
                principalColumn: "FundId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_MonetaryFunds_MonetaryFundId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_MonetaryFundId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "MonetaryFundId",
                table: "Budgets");
        }
    }
}
