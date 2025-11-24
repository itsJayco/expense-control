using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "MonetaryFunds");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseTypeId",
                table: "MonetaryFunds",
                type: "int",
                nullable: false,
                defaultValue: 1);

            // Corregir antiguos registros antes de aplicar FK
            migrationBuilder.Sql("UPDATE MonetaryFunds SET ExpenseTypeId = 1 WHERE ExpenseTypeId = 0");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryFunds_ExpenseTypeId",
                table: "MonetaryFunds",
                column: "ExpenseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryFunds_ExpenseTypes_ExpenseTypeId",
                table: "MonetaryFunds",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "ExpenseTypeId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonetaryFunds_ExpenseTypes_ExpenseTypeId",
                table: "MonetaryFunds");

            migrationBuilder.DropIndex(
                name: "IX_MonetaryFunds_ExpenseTypeId",
                table: "MonetaryFunds");

            migrationBuilder.DropColumn(
                name: "ExpenseTypeId",
                table: "MonetaryFunds");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MonetaryFunds",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
