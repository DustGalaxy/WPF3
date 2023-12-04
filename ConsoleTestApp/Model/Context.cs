using ConsoleTestApp.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model.Entities;

namespace Test.Model
{
    class Context : DbContext
    {
        /// <summary>
        /// Клас що визначає та конфігурує роботу EntityFramework Core (EFC)
        /// </summary>
        public Context() 
        {
            
        }

        // конфігуруємо підключення до БД
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=NewDatabase;username=wpfapp;password=1111");
        }

        // реєструємо сутності для EFC
        public DbSet<User> Users { get; set; }
        public DbSet<QuestionTheme> QuestionThemes { get; set; }
        public DbSet<Questions> Qustions { get; set; }
        // public DbSet<WrongAnswers> Answers { get; set; }
        public DbSet<Results> Results { get; set; }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<TimeOuts> TimeOuts { get; set; }
        public DbSet<Mail> Mails { get; set; }
    }
}
