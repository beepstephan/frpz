using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frpz.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
        public bool isBlocked { get; set; }

        public ApplicationDbContext ApplicationDbContext
        {
            get => default;
            set
            {
            }
        }
    }
}
