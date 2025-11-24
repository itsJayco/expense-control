namespace Backend.Api.DTOs
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; } = string.Empty;

        public int MonetaryFundId { get; set; }
        public string MonetaryFundName { get; set; } = string.Empty;
    }
}
