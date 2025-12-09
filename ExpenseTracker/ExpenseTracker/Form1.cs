using ExpenceTracker.Data;

namespace ExpenceTracker
{
    public partial class Form1 : Form
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public Form1(ExpenseTrackerDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var count = _dbContext.Categories.Count();
                MessageBox.Show($"✅ Database connected! Categories: {count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error: {ex.Message}");
            }
        }
    }
}