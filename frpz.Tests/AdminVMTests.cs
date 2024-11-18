using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using frpz.Models;
using frpz.ViewModels;

namespace frpz.Tests.ViewModelTests
{
    public class AdminVMTests
    {
        [Fact]
        public void CanCreateTestWithQuestionsAndAnswers()
        {
            // Створюємо тест
            var test = new Test
            {
                Title = "Перший тест",
                Description = "Це тест на базові знання"
            };

            // Додаємо питання до тесту
            var question1 = new Question
            {
                Text = "Що таке .NET?",
                Test = test // Встановлюємо зв'язок з тестом
            };
            var question2 = new Question
            {
                Text = "Що таке C#?",
                Test = test
            };

            test.Questions = new List<Question> { question1, question2 };

            // Додаємо відповіді до питань
            var answer1 = new Answer
            {
                Text = ".NET - це платформа",
                IsCorrect = true,
                Question = question1
            };
            var answer2 = new Answer
            {
                Text = ".NET - це мова програмування",
                IsCorrect = false,
                Question = question1
            };

            question1.Answers = new List<Answer> { answer1, answer2 };

            // Перевіряємо зв’язки
            Assert.Equal("Перший тест", test.Title);
            Assert.Equal(2, test.Questions.Count);
            Assert.Equal("Що таке .NET?", test.Questions.First().Text);
            Assert.Equal(2, test.Questions.First().Answers.Count);
            Assert.True(test.Questions.First().Answers.First().IsCorrect);
        }

        [Fact]
        public void CanCreateUserTestResult()
        {
            // Створюємо користувача
            var user = new User
            {
                Username = "student1",
                Email = "student1@example.com",
                PasswordHash = "hashed_password",
                Role = "Користувач",
                isBlocked = false
            };

            // Створюємо тест та результати
            var test = new Test
            {
                Title = "Фінальний тест",
                Description = "Тест на завершення курсу"
            };

            var userTestResult = new UserTestResult
            {
                User = user,
                Test = test,
                Score = 92.5,
                DateTaken = DateTime.Now
            };

            // Перевіряємо значення
            Assert.Equal("student1", user.Username);
            Assert.Equal("Фінальний тест", userTestResult.Test.Title);
            Assert.Equal(92.5, userTestResult.Score);
            Assert.Equal(user, userTestResult.User);
        }
    }
}