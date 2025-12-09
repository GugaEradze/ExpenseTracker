using System.Transactions;

namespace ExpenceTracker.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public DateTime CreatedAt { get; set; }

        public Category()
        {
            CreatedAt = DateTime.Now;
            IsDefault = false;
        }
    }
}