using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF3.Model.Entities
{
    class TimeOuts
    {
        /// <summary>
        /// Клас моделі що преставляє сутність у БД 
        /// Сутність що зберігає данні про часові обмеження на проходження тестів користувачами
        /// </summary>
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }


        [ForeignKey(nameof(Tests))]
        public int TestId { get; set; }
        public Tests Test { get; set; }

        public DateTime ToUnblockDate { get; set; }
    }
}
