using System.ComponentModel.DataAnnotations.Schema;

namespace WPF3.Model.Entities
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
