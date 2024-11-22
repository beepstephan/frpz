using frpz.Models;
using frpz.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frpz.Tests
{
    public class TestTakingVMTests
    {
        [Fact]
        public void CanLoadAvailableTests()
        {
            // Захардкоджені тести
            var tests = new List<Test>
            {
                new Test { Id = 1, Title = "Тест 1", Description = "Опис тесту 1" },
                new Test { Id = 2, Title = "Тест 2", Description = "Опис тесту 2" }
            };

            var currentUser = new User { Id = 1, Username = "TestUser" };

            // Створюємо ViewModel
            var viewModel = new TestTakingVM(currentUser, tests);

            // Перевіряємо, що тести завантажені правильно
            Assert.Equal(2, viewModel.AvailableTests.Count);
            Assert.Equal("Тест 1", viewModel.AvailableTests[0].Title);
            Assert.Equal("Тест 2", viewModel.AvailableTests[1].Title);
        }
    }
}

