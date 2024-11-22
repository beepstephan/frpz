using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frpz.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ApplicationDbContext ApplicationDbContext
        {
            get => default;
            set
            {
            }
        }
    }
}
