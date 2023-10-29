using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3.Model.Entities
{
    class QuestionTheme
    {
        /// <summary>
        /// Клас моделі що представляє сутність у БД 
        /// Сутність що зберігає данні теми для питань
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Questions> Qustions { get; set; }
    }
}
