using frpz.Helpers;
using frpz.Models;
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

        public async Task CreateUserAsync(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task RegisterUserAsync(string username, string email, string password, string role = "Користувач")
        {
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = PasswordHelper.passwordHasher(password),
                Role = role,
                isBlocked = false
            };
            await CreateUserAsync(user);
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
