namespace ExpenceTracker.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Budget()
        {
            CreatedAt = DateTime.Now;
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
        }
    }
}