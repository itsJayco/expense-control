namespace Backend.Api.DTOs
{
	public class ExpenseDto
	{
		public int ExpenseId { get; set; }
		public int BudgetId { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; } = string.Empty;
	}
}
