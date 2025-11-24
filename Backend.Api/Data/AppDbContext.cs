using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;

namespace Backend.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ENTITIES
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<MonetaryFund> MonetaryFunds { get; set; }
        public DbSet<Budget> Budgets { get; set; } = null!;
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<ExpenseHeader> ExpenseHeaders { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetails { get; set; }

        // NUEVA ENTIDAD
        public DbSet<Deposit> Deposits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ----------------------
            // ExpenseType
            // ----------------------
            modelBuilder.Entity<ExpenseType>()
                .HasIndex(t => t.Code)
                .IsUnique();

            // ----------------------
            // MonetaryFund
            // ----------------------
            modelBuilder.Entity<MonetaryFund>()
                .HasIndex(f => f.Code)
                .IsUnique();

            modelBuilder.Entity<MonetaryFund>()
                .Property(f => f.Balance)
                .HasColumnType("decimal(18,2)");

            // ----------------------
            // Budget
            // ----------------------
            modelBuilder.Entity<Budget>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Budget>()
                .HasIndex(b => new { b.Year, b.Month, b.ExpenseTypeId })
                .IsUnique();

            // ----------------------
            // Expense
            // ----------------------
            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Budget)
                .WithMany()
                .HasForeignKey(e => e.BudgetId)
                .OnDelete(DeleteBehavior.Restrict);

            // ----------------------
            // ExpenseHeader & ExpenseDetail
            // ----------------------
            modelBuilder.Entity<ExpenseHeader>()
                .HasMany(h => h.Details)
                .WithOne(d => d.ExpenseHeader)
                .HasForeignKey(d => d.ExpenseHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExpenseDetail>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18,2)");

            // ExpenseDetail -> Budget
            modelBuilder.Entity<ExpenseDetail>()
                .HasOne(d => d.Budget)
                .WithMany()
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.Restrict);

            // ExpenseDetail -> ExpenseType
            modelBuilder.Entity<ExpenseDetail>()
                .HasOne(d => d.ExpenseType)
                .WithMany()
                .HasForeignKey(d => d.ExpenseTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ----------------------
            // Deposit
            // ----------------------
            modelBuilder.Entity<Deposit>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Deposit>()
                .HasOne(d => d.MonetaryFund)
                .WithMany()
                .HasForeignKey(d => d.MonetaryFundId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
