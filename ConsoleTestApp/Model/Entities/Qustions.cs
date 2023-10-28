using System;
using System.Collections.Generic;
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
        public int Theme { get; set; }

        public string ImageSrc { get; set; }


        public ICollection<WrongAnswers> Answers { get; set; }
    }
}
