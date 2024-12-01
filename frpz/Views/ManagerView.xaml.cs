using System.Windows;
using frpz.ViewModels;

namespace frpz.Views
{
    public partial class ManagerView : Window
    {
        private readonly ManagerVM _viewModel;

        public ManagerView()
        {
            InitializeComponent();
            _viewModel = new ManagerVM();
            DataContext = _viewModel;
        }

        private void ViewTestHistory_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedUser != null)
            {
                _viewModel.LoadTestResultsForSelectedUser();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть користувача зі списку.");
            }
        }
    }
}