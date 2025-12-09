using ExpenceTracker.Interfaces;

namespace ExpenceTracker
{
    public partial class Form1 : Form
    {
        private readonly IUnitOfWork _unitOfWork;

        // Constructor - DI ახლა UnitOfWork-ს იძლევა
        public Form1(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var categoryCount = await _unitOfWork.Categories.CountAsync();

                var categories = await _unitOfWork.Categories.GetAllAsync();

                var categoryNames = string.Join(", ", categories.Select(c => c.Name));

                MessageBox.Show(
                    $"✅ Database connected!\n\n" +
                    $"Categories: {categoryCount}\n\n" +
                    $"{categoryNames}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}