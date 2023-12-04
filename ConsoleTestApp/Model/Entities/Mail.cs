using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model.Entities;

namespace ConsoleTestApp.Model.Entities
{
    class Mail
    {
        public int Id { get; set; }
        public string Message { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
