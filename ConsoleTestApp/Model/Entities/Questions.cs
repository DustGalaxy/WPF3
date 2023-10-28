using ConsoleTestApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3.Model.Entities
{
    class Questions
    {
        /// <summary>
        /// Клас моделі що преставляє сутність у БД 
        /// Сутність що зберігає данні про питання до тестів
        /// </summary>
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }

        [ForeignKey(nameof(QuestionTheme))]
        public int ThemeId { get; set; }
        public QuestionTheme? QuestionTheme { get; set; }

        public string? ImageSrc { get; set; }


        public ICollection<WrongAnswers> Answers { get; set; }
        public ICollection<Tests> Tests { get; set; }
    }
}
