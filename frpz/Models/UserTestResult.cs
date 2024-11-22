using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frpz.Models
{
    public class UserTestResult
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public double Score { get; set; } 
        public DateTime DateTaken { get; set; }

        public ApplicationDbContext ApplicationDbContext
        {
            get => default;
            set
            {
            }
        }
    }
}
