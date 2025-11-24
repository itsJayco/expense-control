namespace Backend.Api.DTOs.Deposits
{
    public class DepositDto
    {
        public int DepositId { get; set; }

        public int MonetaryFundId { get; set; }
        public string MonetaryFundName { get; set; } = string.Empty;
        public string MonetaryFundCode { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DepositDate { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
