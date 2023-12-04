

using System.Collections.Generic;

namespace WPF3.Model.Entities
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
        public int Count { get; set; }
        public bool IsActived { get; set; } 
        public ICollection<Questions> Questions { get; set; }
    }
}
