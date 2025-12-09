using ExpenceTracker.Models;

namespace ExpenseTracker.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Transaction> Transactions { get; }
        IRepository<Budget> Budgets { get; }
        IRepository<Category> Categories { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}