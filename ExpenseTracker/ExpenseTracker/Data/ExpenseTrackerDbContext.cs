using ExpenceTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExpenceTracker.Data
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Budget>()
                .HasOne(b => b.Category)
                .WithMany()
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Budget>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.Date);

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.CategoryId);

            modelBuilder.Entity<Budget>()
                .HasIndex(b => new { b.CategoryId, b.Month, b.Year })
                .IsUnique();

            var seedDate = new DateTime(2024, 1, 1);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Food & Dining",
                    Icon = "🍔",
                    Color = "#FF6B6B",
                    IsDefault = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 2,
                    Name = "Transportation",
                    Icon = "🚗",
                    Color = "#4ECDC4",
                    IsDefault = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 3,
                    Name = "Shopping",
                    Icon = "🛍️",
                    Color = "#95E1D3",
                    IsDefault = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 4,
                    Name = "Entertainment",
                    Icon = "🎬",
                    Color = "#F38181",
                    IsDefault = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 5,
                    Name = "Bills & Utilities",
                    Icon = "💡",
                    Color = "#FFA07A",
                    IsDefault = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 6,
                    Name = "Healthcare",
                    Icon = "🏥",
                    Color = "#98D8C8",
                    IsDefault = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 7,
                    Name = "Income",
                    Icon = "💰",
                    Color = "#6BCF7F",
                    IsDefault = true,
                    CreatedAt = seedDate
                }
            );
        }
    }
}