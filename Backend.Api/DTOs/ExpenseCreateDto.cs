namespace Backend.Api.DTOs
{
    public class ExpenseCreateDto
    {
        public int BudgetId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
