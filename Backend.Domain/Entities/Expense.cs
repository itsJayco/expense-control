namespace Backend.Domain.Entities
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        public int UserId { get; set; } = 1;

        public int BudgetId { get; set; }
        public Budget? Budget { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
