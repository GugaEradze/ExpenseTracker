using ExpenceTracker.Data;
using ExpenceTracker.Interfaces;
using ExpenceTracker.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExpenseTracker.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExpenseTrackerDbContext _context;
        private IDbContextTransaction? _transaction;
        private IRepository<Transaction>? _transactions;
        private IRepository<Category>? _categories;
        private IRepository<Budget>? _budgets;

        public UnitOfWork(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public IRepository<Transaction> Transactions =>
            _transactions ??= new Repository<Transaction>(_context);

        public IRepository<Budget> Budgets =>
            _budgets ??= new Repository<Budget>(_context);

        public IRepository<Category> Categories =>
            _categories ??= new Repository<Category>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction");

            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction");

            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}