using ExpenceTracker.Enums;

namespace ExpenceTracker.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Transaction()
        {
            Date = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        public Transaction(decimal amount, string? description, TransactionType transactionType, int categoryId)
            : this()
        {
            Amount = amount;
            Description = description;
            TransactionType = transactionType;
            CategoryId = categoryId;
        }
    }
}