using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseHeaders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseHeaders",
                columns: table => new
                {
                    ExpenseHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonetaryFundId = table.Column<int>(type: "int", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommerceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseHeaders", x => x.ExpenseHeaderId);
                    table.ForeignKey(
                        name: "FK_ExpenseHeaders_MonetaryFunds_MonetaryFundId",
                        column: x => x.MonetaryFundId,
                        principalTable: "MonetaryFunds",
                        principalColumn: "FundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseDetails",
                columns: table => new
                {
                    ExpenseDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseHeaderId = table.Column<int>(type: "int", nullable: false),
                    ExpenseTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDetails", x => x.ExpenseDetailId);
                    table.ForeignKey(
                        name: "FK_ExpenseDetails_ExpenseHeaders_ExpenseHeaderId",
                        column: x => x.ExpenseHeaderId,
                        principalTable: "ExpenseHeaders",
                        principalColumn: "ExpenseHeaderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseDetails_ExpenseTypes_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "ExpenseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDetails_ExpenseHeaderId",
                table: "ExpenseDetails",
                column: "ExpenseHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDetails_ExpenseTypeId",
                table: "ExpenseDetails",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseHeaders_MonetaryFundId",
                table: "ExpenseHeaders",
                column: "MonetaryFundId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseDetails");

            migrationBuilder.DropTable(
                name: "ExpenseHeaders");
        }
    }
}
