using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Model
{
    class Mail
    {
        public int Id { get; set; }
        public string Message { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
    interface IDataMail
    {
        public void CreateMail(Mail mail);
        public void UpdateMail(Mail mail);
        public void DeleteMail(Mail mail);
        public Dictionary<int, Mail> GetMailsDictionary(int userId);
    }

    class ServiceMail : IDataMail
    {
        public void CreateMail(Mail mail)
        {
            using (Context context = new Context())
            {
                context.Mails.Add(mail);
                context.SaveChanges();
            }
        }

        public void UpdateMail(Mail mail)
        {
            using (Context context = new Context())
            {
                context.Mails.Update(mail);
                context.SaveChanges();
            }
        }

        public void DeleteMail(Mail mail)
        {
            using (Context context = new Context())
            {
                context.Mails.Remove(mail);
                context.SaveChanges();
            }
        }

        public Dictionary<int, Mail> GetMailsDictionary(int userId)
        {
            List<Mail> mails = new List<Mail>();
            using (Context context = new Context())
            {
                mails = context.Mails.Where(p => p.UserId == userId).ToList();
            }
            Dictionary<int, Mail> pairs = new Dictionary<int, Mail>();
            foreach (var item in mails)
            {
                pairs.Add(item.Id, item);
            }
            return pairs;
        }
    }

}
