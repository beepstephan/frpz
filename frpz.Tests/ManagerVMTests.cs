using frpz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frpz.Tests
{
    public class ManagerVMTests
    {
        [Fact]
        public void CanCreateUserWithTestResults()
        {
            // Створюємо користувача з роллю "Користувач"
            var user = new User
            {
                Username = "user1",
                Email = "user1@example.com",
                PasswordHash = "hashed_password",
                Role = "Користувач",
                isBlocked = false
            };

            // Створюємо тести та результати тестів для користувача (але не в моделі User)
            var test1 = new Test
            {
                Title = "Тест 1",
                Description = "Тест для перевірки знань з .NET"
            };

            var test2 = new Test
            {
                Title = "Тест 2",
                Description = "Тест для перевірки знань з C#"
            };

            var userTestResults = new List<UserTestResult>
            {
                new UserTestResult
                {
                    User = user,
                    Test = test1,
                    Score = 85.0,
                    DateTaken = DateTime.Now.AddDays(-10)
                },
                new UserTestResult
                {
                    User = user,
                    Test = test2,
                    Score = 90.0,
                    DateTaken = DateTime.Now.AddDays(-5)
                }
            };

            // Перевіряємо властивості користувача
            Assert.Equal("user1", user.Username);
            Assert.Equal("user1@example.com", user.Email);
            Assert.Equal("Користувач", user.Role);
            Assert.False(user.isBlocked);

            // Перевіряємо результати тестів
            Assert.Equal(2, userTestResults.Count);
            Assert.Equal("Тест 1", userTestResults[0].Test.Title);
            Assert.Equal(85.0, userTestResults[0].Score);
            Assert.Equal("Тест 2", userTestResults[1].Test.Title);
            Assert.Equal(90.0, userTestResults[1].Score);
        }

        [Fact]
        public void CanSelectUserAndLoadTestResults()
        {
            // Створюємо користувачів
            var user1 = new User
            {
                Id = 1,
                Username = "user1",
                Email = "user1@example.com",
                PasswordHash = "hashed_password1",
                Role = "Користувач",
                isBlocked = false
            };

            var user2 = new User
            {
                Id = 2,
                Username = "user2",
                Email = "user2@example.com",
                PasswordHash = "hashed_password2",
                Role = "Менеджер",
                isBlocked = false
            };

            // Створюємо результати тестів для користувача 1 (зберігаємо окремо від моделі User)
            var test = new Test
            {
                Title = "Фінальний тест",
                Description = "Тест на завершення курсу"
            };

            var userTestResults = new List<UserTestResult>
            {
                new UserTestResult
                {
                    User = user1,
                    Test = test,
                    Score = 95.0,
                    DateTaken = DateTime.Now
                }
            };

            // Перевіряємо зв’язки та властивості
            Assert.Equal("Фінальний тест", userTestResults.First().Test.Title);
            Assert.Equal(95.0, userTestResults.First().Score);
            Assert.Equal("Користувач", user1.Role);
            Assert.NotEqual(user1.Id, user2.Id); // Перевірка на різні Id
        }
    }
}
