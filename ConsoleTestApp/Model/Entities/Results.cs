using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF3.Model.Entities;

namespace ConsoleTestApp.Model.Entities
{
    class Results
    {
        /// <summary>
        /// Клас моделі що преставляє сутність у БД 
        /// Сутність що зберігає данні о результатах тестів що завершили користувачі
        /// </summary>       
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Result { get; set; }
        [ForeignKey(nameof(Test))]
        public int TestId { get; set; }
        public Tests Tests { get; set; }
    }
}
