using ExpenceTracker.Enums;
using ExpenceTracker.Models;
using ExpenceTracker.Services.Interfaces;
using ExpenseTracker.Services.Interfaces;

namespace ExpenceTracker.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction.Amount <= 0)
                throw new ArgumentException("Amount must be greater than 0");

            transaction.CreatedAt = DateTime.Now;
            transaction.Date = transaction.Date == default ? DateTime.Now : transaction.Date;

            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _unitOfWork.Transactions.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _unitOfWork.Transactions.GetAllAsync();
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction.Amount <= 0)
                throw new ArgumentException("Amount must be greater than 0");

            transaction.UpdatedAt = DateTime.Now;

            await _unitOfWork.Transactions.UpdateAsync(transaction);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);

            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID {id} not found");

            await _unitOfWork.Transactions.DeleteAsync(transaction);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _unitOfWork.Transactions.FindAsync(
                t => t.Date >= startDate && t.Date <= endDate
            );
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId)
        {
            return await _unitOfWork.Transactions.FindAsync(
                t => t.CategoryId == categoryId
            );
        }

        public async Task<IEnumerable<Transaction>> GetExpensesAsync()
        {
            return await _unitOfWork.Transactions.FindAsync(
                t => t.TransactionType == TransactionType.Expense
            );
        }

        public async Task<IEnumerable<Transaction>> GetIncomeAsync()
        {
            return await _unitOfWork.Transactions.FindAsync(
                t => t.TransactionType == TransactionType.Income
            );
        }

        public async Task<decimal> GetTotalExpensesAsync()
        {
            var expenses = await GetExpensesAsync();
            return expenses.Sum(t => t.Amount);
        }

        public async Task<decimal> GetTotalIncomeAsync()
        {
            var income = await GetIncomeAsync();
            return income.Sum(t => t.Amount);
        }

        public async Task<decimal> GetBalanceAsync()
        {
            var totalIncome = await GetTotalIncomeAsync();
            var totalExpenses = await GetTotalExpensesAsync();
            return totalIncome - totalExpenses;
        }

        public async Task<decimal> GetExpensesByCategoryAsync(int categoryId)
        {
            var transactions = await _unitOfWork.Transactions.FindAsync(
                t => t.CategoryId == categoryId && t.TransactionType == TransactionType.Expense
            );
            return transactions.Sum(t => t.Amount);
        }
    }
}