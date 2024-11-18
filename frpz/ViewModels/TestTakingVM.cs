using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using frpz.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Timers = System.Timers;

namespace frpz.ViewModels
{
    public class TestTakingVM : INotifyPropertyChanged
    {
        private Question _currentQuestion;

        public event PropertyChangedEventHandler PropertyChanged;

        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                if (_currentQuestion != value)
                {
                    _currentQuestion = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly ApplicationDbContext _dbContext;
        private Timers.Timer _testTimer;
        private int _timeRemaining;
        private int _currentQuestionIndex;

        public TestTakingVM(User currentUser)
        {
            _dbContext = new ApplicationDbContext();
            CurrentUser = currentUser;
            Questions = new List<Question>();
            UserAnswers = new Dictionary<int, int>();
        }

        public User CurrentUser { get; private set; }
        public List<Test> AvailableTests { get; private set; }
        public Test SelectedTest { get; set; }
        public List<Question> Questions { get; private set; }
        // public Question CurrentQuestion { get; private set; }
        public int? SelectedAnswerId { get; set; }
        public Dictionary<int, int> UserAnswers { get; private set; }
        public int CorrectAnswersCount { get; private set; }

        public void LoadAvailableTests()
        {
            AvailableTests = _dbContext.Tests.ToList();
        }

        public void LoadTest(int testId)
        {
            SelectedTest = _dbContext.Tests.FirstOrDefault(t => t.Id == testId);
            if (SelectedTest != null)
            {
                Questions = _dbContext.Questions
                    .Where(q => q.TestId == testId)
                    .Include(q => q.Answers)
                    .ToList();
            }
        }

        // Метод для оновлення поточного питання
        public void SetCurrentQuestion(int index)
        {
            if (index >= 0 && index < Questions.Count)
            {
                _currentQuestionIndex = index;
                CurrentQuestion = Questions[index];  // Тепер це автоматично викликає OnPropertyChanged
                SelectedAnswerId = UserAnswers.ContainsKey(CurrentQuestion.Id) ? UserAnswers[CurrentQuestion.Id] : (int?)null;
            }
        }

        public void PreviousQuestion()
        {
            if (_currentQuestionIndex > 0)
            {
                _currentQuestionIndex--;
                SetCurrentQuestion(_currentQuestionIndex);  // Оновлюємо питання
            }
        }

        public void NextQuestion()
        {
            if (_currentQuestionIndex < Questions.Count - 1)
            {
                _currentQuestionIndex++;
                SetCurrentQuestion(_currentQuestionIndex);  // Оновлюємо питання
            }
        }

        public void StartTest(int timeLimitInMinutes)
        {
            _timeRemaining = timeLimitInMinutes * 60;
            _testTimer = new Timers.Timer(1000);
            _testTimer.Elapsed += OnTimerTick;
            _testTimer.Start();
        }

        private void OnTimerTick(object sender, Timers.ElapsedEventArgs e)
        {
            _timeRemaining--;
            if (_timeRemaining <= 0)
            {
                _testTimer.Stop();
                FinishTest();
            }
        }

        public void SubmitAnswer(int questionId, int answerId)
        {
            if (!UserAnswers.ContainsKey(questionId))
            {
                UserAnswers.Add(questionId, answerId);
            }
            else
            {
                UserAnswers[questionId] = answerId;
            }
        }

        public void FinishTest()
        {
            CalculateTestResult();
            SaveTestResult();
        }

        private void CalculateTestResult()
        {
            CorrectAnswersCount = UserAnswers
                .Count(ua =>
                    Questions.FirstOrDefault(q => q.Id == ua.Key)?.Answers
                    .FirstOrDefault(a => a.Id == ua.Value)?.IsCorrect == true);
        }

        private void SaveTestResult()
        {
            var userTestResult = new UserTestResult
            {
                UserId = CurrentUser.Id,
                TestId = SelectedTest.Id,
                Score = (CorrectAnswersCount / (double)Questions.Count) * 100,
                DateTaken = DateTime.UtcNow
            };

            _dbContext.UserTestResults.Add(userTestResult);
            _dbContext.SaveChanges();
        }
    }
}
