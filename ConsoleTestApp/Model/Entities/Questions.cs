using Test.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model.Entities
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

        public string? WAns1 { get; set; }
        public string? WAns2 { get; set; }
        public string? WAns3 { get; set; }
        public ICollection<Tests> Tests { get; set; }
    }
}
