using frpz.ViewModels;
using System.Windows;


namespace frpz.Views
{
    /// <summary>
    /// Логика взаимодействия для TestResultView.xaml
    /// </summary>
    public partial class TestResultView : Window
    {
        public TestResultView(TestResultVM viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            var testSelectionView = new TestSelectionView();
            testSelectionView.Show();
            this.Close();
        }
    }
}
