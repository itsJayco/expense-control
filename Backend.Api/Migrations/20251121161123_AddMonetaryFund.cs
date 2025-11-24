using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMonetaryFund : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExpenseTypes_Code",
                table: "ExpenseTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MonetaryFunds",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MonetaryFunds",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "MonetaryFunds",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MonetaryFunds",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MonetaryFunds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ExpenseTypes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryFunds_Code",
                table: "MonetaryFunds",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_Code",
                table: "ExpenseTypes",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MonetaryFunds_Code",
                table: "MonetaryFunds");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseTypes_Code",
                table: "ExpenseTypes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "MonetaryFunds");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MonetaryFunds");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MonetaryFunds");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MonetaryFunds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MonetaryFunds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ExpenseTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_Code",
                table: "ExpenseTypes",
                column: "Code",
                unique: true);
        }
    }
}
