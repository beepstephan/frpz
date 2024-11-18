using System.Windows;
using System.Windows.Controls;
using frpz.ViewModels;
using frpz.Models;
using frpz.Helpers;

namespace frpz.Views
{
    public partial class AdminView : Window
    {
        private readonly AdminVM _viewModel;

        public AdminView()
        {
            InitializeComponent();
            _viewModel = new AdminVM();
            DataContext = _viewModel;
        }

        private async void AddTest_Click(object sender, RoutedEventArgs e)
        {
            string title = TestTitleTextBox.Text;
            string description = TestDescriptionTextBox.Text;

            await _viewModel.CreateTestAsync(title, description);
            MessageBox.Show("Тест додано!");
            TestTitleTextBox.Clear();
            TestDescriptionTextBox.Clear();
        }

        private async void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            string questionText = QuestionTextBox.Text;

            await _viewModel.AddQuestionToCurrentTestAsync(questionText);
            MessageBox.Show("Питання додано!");
            QuestionTextBox.Clear();
        }

        private async void AddAnswer_Click(object sender, RoutedEventArgs e)
        {
            string answerText = AnswerTextBox.Text;
            bool isCorrect = IsCorrectAnswerCheckBox.IsChecked == true;

            await _viewModel.AddAnswerToCurrentQuestionAsync(answerText, isCorrect);
            MessageBox.Show("Відповідь додано!");
            AnswerTextBox.Clear();
            IsCorrectAnswerCheckBox.IsChecked = false;
        }

        private void QuestionsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _viewModel.SelectedQuestion = (Question)QuestionsListBox.SelectedItem;
            _viewModel.LoadAnswersForSelectedQuestion();
        }
    }
}