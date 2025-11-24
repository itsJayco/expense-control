namespace Backend.Api.DTOs
{
    public class BudgetCreateDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public int ExpenseTypeId { get; set; }
        public int MonetaryFundId { get; set; }
    }
}
