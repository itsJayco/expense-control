using System;
using System.Collections.Generic;

namespace Backend.Api.DTOs
{
    public class ExpenseHeaderCreateDto
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public int MonetaryFundId { get; set; }
        public string Observations { get; set; } = string.Empty;
        public string CommerceName { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public List<ExpenseDetailCreateDto> Details { get; set; } = new List<ExpenseDetailCreateDto>();
    }

    public class ExpenseDetailCreateDto
    {
        public int BudgetId { get; set; }
        public decimal Amount { get; set; }
    }
}
