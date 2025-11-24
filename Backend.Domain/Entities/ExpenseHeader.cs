using System;
using System.Collections.Generic;

namespace Backend.Domain.Entities
{
	public class ExpenseHeader
	{
		public int ExpenseHeaderId { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public int MonetaryFundId { get; set; }
		public string Observations { get; set; } = string.Empty;
		public string CommerceName { get; set; } = string.Empty;
		public string DocumentType { get; set; } = string.Empty;

		// Navegación
		public MonetaryFund? MonetaryFund { get; set; }
		public ICollection<ExpenseDetail>? Details { get; set; }
	}
}
