using ExpenceTracker.Services.Interfaces;
using ExpenceTracker.Models;
using ExpenceTracker.Enums;

namespace ExpenceTracker
{
    public partial class Form1 : Form
    {
        private readonly ITransactionService _transactionService;

        public Form1(ITransactionService transactionService)
        {
            InitializeComponent();
            _transactionService = transactionService;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ ტესტი 1: ბალანსი (ცარიელი database)
                var initialBalance = await _transactionService.GetBalanceAsync();

                // ✅ ტესტი 2: ტრანზაქციის დამატება
                var testTransaction = new Transaction
                {
                    Amount = 100.50m,
                    Description = "Test Expense - Lunch",
                    TransactionType = TransactionType.Expense,
                    CategoryId = 1, // Food & Dining
                    Date = DateTime.Now
                };

                await _transactionService.AddTransactionAsync(testTransaction);

                // ✅ ტესტი 3: Statistics
                var totalExpenses = await _transactionService.GetTotalExpensesAsync();
                var totalIncome = await _transactionService.GetTotalIncomeAsync();
                var newBalance = await _transactionService.GetBalanceAsync();

                // ✅ ტესტი 4: ყველა ტრანზაქცია
                var allTransactions = await _transactionService.GetAllTransactionsAsync();

                MessageBox.Show(
                    $"✅ TransactionService Works!\n\n" +
                    $"Initial Balance: {initialBalance:C}\n" +
                    $"Total Income: {totalIncome:C}\n" +
                    $"Total Expenses: {totalExpenses:C}\n" +
                    $"New Balance: {newBalance:C}\n\n" +
                    $"Total Transactions: {allTransactions.Count()}\n\n" +
                    $"Test transaction added successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Error: {ex.Message}\n\n{ex.InnerException?.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}