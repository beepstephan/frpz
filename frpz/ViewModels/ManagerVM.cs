using frpz.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace frpz.ViewModels
{
    public class ManagerVM : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _dbContext;

        public ManagerVM()
        {
            _dbContext = new ApplicationDbContext();
            Users = new ObservableCollection<User>();
            UserTestResults = new ObservableCollection<UserTestResult>();
            LoadUsersWithRoleUser();
        }

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<UserTestResult> UserTestResults { get; set; }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedUserStatus)); // Оновлюємо статус користувача
                }
            }
        }

        public string SelectedUserStatus =>
            SelectedUser == null
                ? "Користувача не вибрано"
                : SelectedUser.BlockedUntil.HasValue && SelectedUser.BlockedUntil > DateTime.UtcNow
                    ? $"Заблоковано до {SelectedUser.BlockedUntil.Value.ToString("yyyy-MM-dd HH:mm:ss")}"
                    : "Активний";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadUsersWithRoleUser()
        {
            Users.Clear();
            var users = _dbContext.Users.Where(u => u.Role == "Користувач").ToList();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        public void LoadTestResultsForSelectedUser()
        {
            if (SelectedUser != null)
            {
                UserTestResults.Clear();
                var testResults = _dbContext.UserTestResults
                    .Include(tr => tr.Test)
                    .Where(tr => tr.UserId == SelectedUser.Id)
                    .ToList();

                foreach (var result in testResults)
                {
                    UserTestResults.Add(result);
                }
            }
        }
    }
}