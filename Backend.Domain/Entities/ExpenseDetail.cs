using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Backend.Domain.Entities
{
    public class ExpenseDetail
    {
        public int ExpenseDetailId { get; set; }
        public int ExpenseHeaderId { get; set; }
        public int ExpenseTypeId { get; set; }
        public decimal Amount { get; set; }
        public int BudgetId { get; set; }

        // Navegaciones
        public Budget? Budget { get; set; }
        public ExpenseHeader? ExpenseHeader { get; set; }
        public ExpenseType? ExpenseType { get; set; }
    }
}
