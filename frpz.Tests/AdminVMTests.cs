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
            // ��������� ����
            var test = new Test
            {
                Title = "������ ����",
                Description = "�� ���� �� ����� ������"
            };

            // ������ ������� �� �����
            var question1 = new Question
            {
                Text = "�� ���� .NET?",
                Test = test // ������������ ��'���� � ������
            };
            var question2 = new Question
            {
                Text = "�� ���� C#?",
                Test = test
            };

            test.Questions = new List<Question> { question1, question2 };

            // ������ ������ �� ������
            var answer1 = new Answer
            {
                Text = ".NET - �� ���������",
                IsCorrect = true,
                Question = question1
            };
            var answer2 = new Answer
            {
                Text = ".NET - �� ���� �������������",
                IsCorrect = false,
                Question = question1
            };

            question1.Answers = new List<Answer> { answer1, answer2 };

            // ���������� ������
            Assert.Equal("������ ����", test.Title);
            Assert.Equal(2, test.Questions.Count);
            Assert.Equal("�� ���� .NET?", test.Questions.First().Text);
            Assert.Equal(2, test.Questions.First().Answers.Count);
            Assert.True(test.Questions.First().Answers.First().IsCorrect);
        }

        [Fact]
        public void CanCreateUserTestResult()
        {
            // ��������� �����������
            var user = new User
            {
                Username = "student1",
                Email = "student1@example.com",
                PasswordHash = "hashed_password",
                Role = "����������",
                isBlocked = false
            };

            // ��������� ���� �� ����������
            var test = new Test
            {
                Title = "Գ������� ����",
                Description = "���� �� ���������� �����"
            };

            var userTestResult = new UserTestResult
            {
                User = user,
                Test = test,
                Score = 92.5,
                DateTaken = DateTime.Now
            };

            // ���������� ��������
            Assert.Equal("student1", user.Username);
            Assert.Equal("Գ������� ����", userTestResult.Test.Title);
            Assert.Equal(92.5, userTestResult.Score);
            Assert.Equal(user, userTestResult.User);
        }
    }
}