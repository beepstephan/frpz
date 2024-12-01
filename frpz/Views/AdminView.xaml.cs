using System.Windows;
using System.IO;
using System.Windows.Controls;
using frpz.ViewModels;
using frpz.Models;
using System.Windows.Media.Imaging;

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

        // Додати тест
        private async void AddTest_Click(object sender, RoutedEventArgs e)
        {
            string title = TestTitleTextBox.Text;
            string description = TestDescriptionTextBox.Text;

            await _viewModel.CreateTestAsync(title, description);
            MessageBox.Show("Тест додано!");
            TestTitleTextBox.Clear();
            TestDescriptionTextBox.Clear();
        }

        // Редагувати тест
        private async void EditTest_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedTest != null)
            {
                string newTitle = TestTitleTextBox.Text;
                string newDescription = TestDescriptionTextBox.Text;

                await _viewModel.UpdateTestAsync(_viewModel.SelectedTest, newTitle, newDescription);
                MessageBox.Show("Тест оновлено!");
            }
            else
            {
                MessageBox.Show("Оберіть тест для редагування.");
            }
        }

        // Видалити тест
        private async void DeleteTest_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedTest != null)
            {
                if (MessageBox.Show("Ви впевнені, що хочете видалити цей тест?",
                    "Підтвердження", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await _viewModel.DeleteTestAsync(_viewModel.SelectedTest);
                    MessageBox.Show("Тест видалено!");
                }
            }
            else
            {
                MessageBox.Show("Оберіть тест для видалення.");
            }
        }

        // Додати питання
        private async void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            string questionText = QuestionTextBox.Text;

            await _viewModel.AddQuestionToCurrentTestAsync(questionText);
            MessageBox.Show("Питання додано!");
            QuestionTextBox.Clear();
        }

        // Редагувати питання
        private async void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedQuestion != null)
            {
                string newQuestionText = QuestionTextBox.Text;

                await _viewModel.UpdateQuestionAsync(_viewModel.SelectedQuestion, newQuestionText);
                MessageBox.Show("Питання оновлено!");
            }
            else
            {
                MessageBox.Show("Оберіть питання для редагування.");
            }
        }

        // Видалити питання
        private async void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedQuestion != null)
            {
                if (MessageBox.Show("Ви впевнені, що хочете видалити це питання?",
                    "Підтвердження", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await _viewModel.DeleteQuestionAsync(_viewModel.SelectedQuestion);
                    MessageBox.Show("Питання видалено!");
                }
            }
            else
            {
                MessageBox.Show("Оберіть питання для видалення.");
            }
        }


        // Додати відповідь
        private async void AddAnswer_Click(object sender, RoutedEventArgs e)
        {
            string answerText = AnswerTextBox.Text;
            bool isCorrect = IsCorrectAnswerCheckBox.IsChecked == true;

            await _viewModel.AddAnswerToCurrentQuestionAsync(answerText, isCorrect);
            MessageBox.Show("Відповідь додано!");
            AnswerTextBox.Clear();
            IsCorrectAnswerCheckBox.IsChecked = false;
        }

        // Редагувати відповідь
        private async void EditAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedAnswer != null)
            {
                string newAnswerText = AnswerTextBox.Text;
                bool isCorrect = IsCorrectAnswerCheckBox.IsChecked == true;

                await _viewModel.UpdateAnswerAsync(_viewModel.SelectedAnswer, newAnswerText, isCorrect);
                MessageBox.Show("Відповідь оновлено!");
            }
            else
            {
                MessageBox.Show("Оберіть відповідь для редагування.");
            }
        }

        // Видалити відповідь
        private async void DeleteAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedAnswer != null)
            {
                if (MessageBox.Show("Ви впевнені, що хочете видалити цю відповідь?",
                    "Підтвердження", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await _viewModel.DeleteAnswerAsync(_viewModel.SelectedAnswer);
                    MessageBox.Show("Відповідь видалено!");
                }
            }
            else
            {
                MessageBox.Show("Оберіть відповідь для видалення.");
            }
        }

        private async void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Читаємо зображення як масив байтів
                byte[] imageData = File.ReadAllBytes(filePath);

                await _viewModel.SetImageForCurrentQuestion(imageData);

                // Відображаємо обране зображення
                QuestionImage.Source = new BitmapImage(new Uri(filePath));
                MessageBox.Show("Зображення додано!");
            }
        }

        private void QuestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedQuestion = (Question)QuestionsListBox.SelectedItem;
            _viewModel.LoadAnswersForSelectedQuestion();
        }
    }
}