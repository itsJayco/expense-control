using System;

namespace Backend.Domain.Entities
{
    public class Deposit
    {
        public int DepositId { get; set; }

        // FK al fondo monetario
        public int MonetaryFundId { get; set; }
        public MonetaryFund MonetaryFund { get; set; }

        // Datos del depósito
        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime DepositDate { get; set; } = DateTime.UtcNow;

        // Auditoría
        public string CreatedBy { get; set; } = "system";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
