using frpz.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace frpz.ViewModels
{
    public class AdminVM
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminVM()
        {
            _dbContext = new ApplicationDbContext();
            Tests = new ObservableCollection<Test>(_dbContext.Tests.ToList());
            CurrentQuestions = new ObservableCollection<Question>();
            CurrentAnswers = new ObservableCollection<Answer>();
        }

        // Для юніт тестів
        public AdminVM(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Tests = new ObservableCollection<Test>();
            CurrentQuestions = new ObservableCollection<Question>();
            CurrentAnswers = new ObservableCollection<Answer>();
        }

        public ObservableCollection<Test> Tests { get; set; }
        public ObservableCollection<Question> CurrentQuestions { get; set; }
        public ObservableCollection<Answer> CurrentAnswers { get; set; }

        private Test _selectedTest;
        public Test SelectedTest
        {
            get => _selectedTest;
            set
            {
                _selectedTest = value;
                LoadQuestionsForSelectedTest(); // Завантажуємо питання при виборі тесту
            }
        }

        public Question SelectedQuestion { get; set; }
        private Answer _selectedAnswer;
        public Answer SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                _selectedAnswer = value;
            }
        }

        // Створення нового тесту
        public async Task CreateTestAsync(string title, string description)
        {
            var newTest = new Test
            {
                Title = title,
                Description = description
            };

            _dbContext.Tests.Add(newTest);
            await _dbContext.SaveChangesAsync();
            Tests.Add(newTest);
        }

        // Додавання питання
        public async Task AddQuestionToCurrentTestAsync(string questionText)
        {
            if (SelectedTest == null)
            {
                throw new Exception("Не обрано тест для додавання питання.");
            }

            var newQuestion = new Question
            {
                Text = questionText,
                TestId = SelectedTest.Id
            };

            _dbContext.Questions.Add(newQuestion);
            await _dbContext.SaveChangesAsync();
            CurrentQuestions.Add(newQuestion);
        }

        // Додавання відповіді
        public async Task AddAnswerToCurrentQuestionAsync(string answerText, bool isCorrect)
        {
            if (SelectedQuestion == null)
            {
                throw new Exception("Не обрано питання для додавання відповіді.");
            }

            var newAnswer = new Answer
            {
                Text = answerText,
                IsCorrect = isCorrect,
                QuestionId = SelectedQuestion.Id
            };

            _dbContext.Answers.Add(newAnswer);
            await _dbContext.SaveChangesAsync();
            CurrentAnswers.Add(newAnswer);
        }

        // Завантаження питань
        public void LoadQuestionsForSelectedTest()
        {
            if (SelectedTest != null)
            {
                CurrentQuestions.Clear();
                var questions = _dbContext.Questions.Where(q => q.TestId == SelectedTest.Id).ToList();
                foreach (var question in questions)
                {
                    CurrentQuestions.Add(question);
                }

                // Автоматично обираємо перше питання, якщо воно є
                SelectedQuestion = CurrentQuestions.FirstOrDefault();
                LoadAnswersForSelectedQuestion(); // Завантажуємо відповіді для обраного питання
            }
        }

        // Завантаження відповідей
        public void LoadAnswersForSelectedQuestion()
        {
            if (SelectedQuestion != null)
            {
                CurrentAnswers.Clear();
                var answers = _dbContext.Answers.Where(a => a.QuestionId == SelectedQuestion.Id).ToList();
                foreach (var answer in answers)
                {
                    CurrentAnswers.Add(answer);
                }
            }
        }

        // Видалення тесту
        public async Task DeleteTestAsync(Test test)
        {
            _dbContext.Tests.Remove(test);
            await _dbContext.SaveChangesAsync();
            Tests.Remove(test);
        }

        // Редагування тесту
        public async Task UpdateTestAsync(Test test, string newTitle, string newDescription)
        {
            test.Title = newTitle;
            test.Description = newDescription;
            _dbContext.Tests.Update(test);
            await _dbContext.SaveChangesAsync();
            LoadTests();
        }

        // Видалення питання
        public async Task DeleteQuestionAsync(Question question)
        {
            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();
            CurrentQuestions.Remove(question);
        }

        // Додавання зображення до питання
        public async Task SetImageForCurrentQuestion(byte[] imageData)
        {
            if (SelectedQuestion == null)
            {
                throw new Exception("Не обрано питання для додавання зображення.");
            }

            SelectedQuestion.ImageData = imageData;
            _dbContext.Questions.Update(SelectedQuestion);
            await _dbContext.SaveChangesAsync();
        }

        // Редагування питання
        public async Task UpdateQuestionAsync(Question question, string newText)
        {
            question.Text = newText;
            _dbContext.Questions.Update(question);
            await _dbContext.SaveChangesAsync();
            LoadQuestionsForSelectedTest();
        }

        // Видалення відповіді
        public async Task DeleteAnswerAsync(Answer answer)
        {
            _dbContext.Answers.Remove(answer);
            await _dbContext.SaveChangesAsync();
            CurrentAnswers.Remove(answer);
        }

        // Редагування відповіді
        public async Task UpdateAnswerAsync(Answer answer, string newText, bool isCorrect)
        {
            answer.Text = newText;
            answer.IsCorrect = isCorrect;
            _dbContext.Answers.Update(answer);
            await _dbContext.SaveChangesAsync();
            LoadAnswersForSelectedQuestion();
        }

        // Завантаження тестів
        private void LoadTests()
        {
            Tests.Clear();
            var tests = _dbContext.Tests.ToList();
            foreach (var test in tests)
            {
                Tests.Add(test);
            }
        }
    }
}