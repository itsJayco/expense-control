namespace Backend.Domain.Entities
{
    public class Budget
    {
        public int BudgetId { get; set; }

        public int? UserId { get; set; } = 1;

        public int ExpenseTypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }

        public int MonetaryFundId { get; set; }
        public MonetaryFund? MonetaryFund { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}
