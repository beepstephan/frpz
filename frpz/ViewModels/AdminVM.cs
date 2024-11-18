using frpz.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        // для юніт тестив
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

        public async Task AddQuestionToCurrentTestAsync(string questionText)
        {
            if (SelectedTest == null)
            {
                throw new System.Exception("Не обрано тест для додавання питання.");
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

        public async Task AddAnswerToCurrentQuestionAsync(string answerText, bool isCorrect)
        {
            if (SelectedQuestion == null)
            {
                throw new System.Exception("Не обрано питання для додавання відповіді.");
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
            }
        }

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
    }
}
