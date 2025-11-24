namespace Backend.Api.DTOs.Deposits
{
	public class DepositCreateDto
	{
		public int MonetaryFundId { get; set; }
		public string Description { get; set; } = string.Empty;
		public decimal Amount { get; set; }
		public DateTime DepositDate { get; set; }
	}
}
