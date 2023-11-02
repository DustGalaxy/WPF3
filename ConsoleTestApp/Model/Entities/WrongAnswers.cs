using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model.Entities
{
    class WrongAnswers
    {
        /// <summary>
        /// Клас моделі що преставляє сутність у БД 
        /// Сутність що зберігає данні про невірні відповіді до питань
        /// </summary>
        public int Id { get; set; }
        public string? Answer { get; set; }

        [ForeignKey(nameof(Questions))]
        public int QuestionId { get; set; }

        public Questions Questions { get; set; }


    }
}
