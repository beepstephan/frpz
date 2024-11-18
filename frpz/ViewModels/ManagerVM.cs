using frpz.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frpz.ViewModels
{
    public class ManagerVM
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
        public User SelectedUser { get; set; }

        
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
                    .Include(tr => tr.Test) // Завантажуємо пов’язану інформацію про тест
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