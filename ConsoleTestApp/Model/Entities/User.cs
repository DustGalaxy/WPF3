using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    class User
    {
        /// <summary>
        /// Клас моделі що представляє сутність у БД 
        /// Сутність що зберігає данні користувачів
        /// </summary>
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int UserType { get; set; }

    }
}
