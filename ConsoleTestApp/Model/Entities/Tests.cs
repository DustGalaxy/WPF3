

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Model
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
        public List<Questions> Questions { get; set; } = new();
        [NotMapped]
        public List<int> QuestionsId { get; set; } = new();
    }
}
