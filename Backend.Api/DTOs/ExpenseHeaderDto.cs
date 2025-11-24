using System;
using System.Collections.Generic;

namespace Backend.Api.DTOs
{
    public class ExpenseHeaderDto
    {
        public int ExpenseHeaderId { get; set; }
        public DateTime Date { get; set; }
        public int MonetaryFundId { get; set; }
        public string MonetaryFundName { get; set; } = string.Empty;
        public string Observations { get; set; } = string.Empty;
        public string CommerceName { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public List<ExpenseDetailDto> Details { get; set; } = new List<ExpenseDetailDto>();
    }

    public class ExpenseDetailDto
    {
        public int ExpenseDetailId { get; set; }
        public int BudgetId { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonetaryFundName { get; set; } = string.Empty;
    }
}
