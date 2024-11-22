﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frpz.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ApplicationDbContext ApplicationDbContext
        {
            get => default;
            set
            {
            }
        }
    }
}
