using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF3.Model.Entities;

namespace WPF3.Model
{
    class Context : DbContext
    {
        public Context() 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=newera;username=wpfapp;password=1111");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<QuestionTheme> QuestionThemes { get; set; }
        public DbSet<Qustions> Qustions { get; set; }
        public DbSet<WrongAnswers> Answers { get; set; }
    }
}
