using frpz.Models;
using frpz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace frpz.Views
{
    /// <summary>
    /// Interaction logic for TestSelectionView.xaml
    /// </summary>
    public partial class TestSelectionView : Window
    {
        private TestTakingVM _viewModel;

        public TestSelectionView()
        {
            InitializeComponent();
            _viewModel = new TestTakingVM(CurrentUser.LoggedInUser);
            DataContext = _viewModel;

            // Завантажуємо доступні тести
            _viewModel.LoadAvailableTests();
        }

        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedTest != null)
            {
                var testTakingView = new TestTakingView(_viewModel);
                testTakingView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть тест.");
            }
        }
    }
}
