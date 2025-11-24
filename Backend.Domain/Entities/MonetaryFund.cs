using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Entities
{
    public class MonetaryFund
    {
        [Key]
        public int FundId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = null!;

        public int ExpenseTypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }

        public decimal Balance { get; set; } = 0m;

        public bool IsActive { get; set; } = true;
    }

}
