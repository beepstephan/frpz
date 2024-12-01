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
    /// Interaction logic for TestTakinView.xaml
    /// </summary>
    public partial class TestTakingView : Window
    {
        private TestTakingVM _viewModel;

        public TestTakingView(TestTakingVM viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            // Завантажуємо питання для обраного тесту
            _viewModel.LoadTest(_viewModel.SelectedTest.Id);
            _viewModel.SetCurrentQuestion(0); // Починаємо з першого питання
            _viewModel.StartTest(1);
        }

        

        // Обробка зміни вибору відповіді
        private void AnswerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel.CurrentQuestion != null)
            {
                var selectedAnswer = (Answer)((ListBox)sender).SelectedItem;
                if (selectedAnswer != null)
                {
                    _viewModel.SubmitAnswer(_viewModel.CurrentQuestion.Id, selectedAnswer.Id);
                }
            }
        }

        // Обробка натискання кнопки "Попереднє питання"
        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousQuestion();
            UpdateUI();
        }

        // Обробка натискання кнопки "Наступне питання"
        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NextQuestion();
            UpdateUI();
        }

        // Оновлення UI після зміни питання
        private void UpdateUI()
        {
            // Оновлюємо текст питання та відповіді
            var currentQuestion = _viewModel.CurrentQuestion;
            var currentAnswers = currentQuestion?.Answers;
            AnswersList.ItemsSource = currentAnswers;
            AnswersList.SelectedItem = _viewModel.SelectedAnswerId.HasValue
                ? currentAnswers?.FirstOrDefault(a => a.Id == _viewModel.SelectedAnswerId)
                : null;
        }

        // Обробка натискання кнопки "Завершити тест"
        private void FinishTest_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FinishTest();

            var results = _viewModel.GetTestResults();
            var resultViewModel = new TestResultVM(results);

            var resultView = new TestResultView(resultViewModel);
            resultView.Show();

            this.Close();
        }


    }
}