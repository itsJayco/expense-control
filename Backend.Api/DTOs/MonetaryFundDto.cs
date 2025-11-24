namespace Backend.Api.DTOs
{
    public class MonetaryFundDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; } = string.Empty;

        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
