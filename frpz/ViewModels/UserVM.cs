﻿using frpz.Helpers;
using frpz.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frpz.ViewModels
{
    public class UserVM
    {
        private readonly ApplicationDbContext dbContext;

        public UserVM() 
        { 
            dbContext = new ApplicationDbContext();
        }

        public User FindUserByEmail(string email)
        {
            return dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        // Оновлення даних користувача
        public void UpdateUser(User user)
        {
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
        }


        public async Task CreateUserAsync(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task RegisterUserAsync(string username, string email, string password, string role = "Користувач")
        {
            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = PasswordHelper.passwordHasher(password),
                Role = "Користувач",
                isBlocked = false,
                FailedAttempts = 0,
                BlockedUntil = null
            };

            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync();
        }

        public User AuthenticateUser(string email, string password)
        {
            string hashedPassword = PasswordHelper.passwordHasher(password);
            
            return dbContext.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);
        }

        public List<User> GetAllUsers() 
        {
            return dbContext.Users.ToList();
        }

        public async Task UpdateUserAsync(User user) 
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
