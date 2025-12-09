using ExpenceTracker.Models;

namespace ExpenceTracker.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);

        Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId);
        Task<IEnumerable<Transaction>> GetExpensesAsync();
        Task<IEnumerable<Transaction>> GetIncomeAsync();

        Task<decimal> GetTotalExpensesAsync();
        Task<decimal> GetTotalIncomeAsync();
        Task<decimal> GetBalanceAsync();
        Task<decimal> GetExpensesByCategoryAsync(int categoryId);
    }
}