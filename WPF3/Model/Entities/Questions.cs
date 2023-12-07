using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF3.Model.Entities
{
    class Questions
    {
        /// <summary>
        /// Клас моделі що преставляє сутність у БД 
        /// Сутність що зберігає данні про питання до тестів
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }

        [ForeignKey(nameof(QuestionTheme))]
        public int ThemeId { get; set; }
        public QuestionTheme? QuestionTheme { get; set; }

        public string? ImageSrc { get; set; }

        public string? WAns1 { get; set; }
        public string? WAns2 { get; set; }
        public string? WAns3 { get; set; }
        public List<Tests> Tests { get; set; } = new();
    }
}
