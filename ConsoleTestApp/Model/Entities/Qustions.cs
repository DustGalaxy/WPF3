using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3.Model.Entities
{
    class Qustions
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }

        [ForeignKey(nameof(QuestionTheme))]
        public int ThemeId { get; set; }
        public QuestionTheme? QuestionTheme { get; set; }

        public string? ImageSrc { get; set; }


        public ICollection<WrongAnswers> Answers { get; set; }
    }
}
