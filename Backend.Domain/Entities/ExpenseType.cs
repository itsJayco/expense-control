namespace Backend.Domain.Entities
{
    public class ExpenseType
    {
        public int ExpenseTypeId { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}