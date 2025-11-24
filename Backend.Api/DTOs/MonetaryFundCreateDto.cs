namespace Backend.Api.DTOs
{
    public class MonetaryFundCreateDto
    {
        public string Name { get; set; } = null!;
        public int ExpenseTypeId { get; set; }
        public decimal Balance { get; set; } = 0m;
        public bool IsActive { get; set; } = true;
    }
}
