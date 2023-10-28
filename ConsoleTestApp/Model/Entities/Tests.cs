using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF3.Model.Entities;

namespace ConsoleTestApp.Model.Entities
{
    class Tests
    {   
    /// <summary>
    /// Клас моделі що преставляє сутність у БД 
    /// Сутність що зберігає данні про існуючі тести
    /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Questions> Questions { get; set; }
    }
}
