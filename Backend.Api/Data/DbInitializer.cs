using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            if (!context.ExpenseTypes.Any())
            {
                var expenseTypes = new ExpenseType[]
                {
                    new ExpenseType { Code = "ET-0001", Name = "Food", Description = "Food expenses" },
                    new ExpenseType { Code = "ET-0002", Name = "Transport", Description = "Mobility and transport" },
                    new ExpenseType { Code = "ET-0003", Name = "Services", Description = "Utilities: water, electricity, internet" }
                };

                context.ExpenseTypes.AddRange(expenseTypes);
            }

            if (!context.MonetaryFunds.Any())
            {
                int counter = 1;

                var funds = new MonetaryFund[]
                {
                    new MonetaryFund
                    {
                        Code = "MF-0001",
                        Name = "Caja Principal",
                        ExpenseTypeId = 1,
                        Balance = 100000,
                        IsActive = true
                    },
                    new MonetaryFund
                    {
                        Code = "MF-0002",
                        Name = "Banco BBVA",
                        ExpenseTypeId = 2,
                        Balance = 200000,
                        IsActive = true
                    }

                };

                context.MonetaryFunds.AddRange(funds);

            }

            context.SaveChanges();
        }
    }
}
