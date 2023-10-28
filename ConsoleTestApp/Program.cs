// See https://aka.ms/new-console-template for more information
using WPF3.Model;
using WPF3.Model.Entities;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Context context = new Context())
            {
                var usr1 = new User()
                {
                    Name = "Test",
                    Password = "password",
                    Email = "123",
                    Surname = "Test",
                    UserType = 1
                };


                context.Users.Add(usr1);

                var qt = new QuestionTheme()
                {
                    Name = "Лева руля!",
                    Description = "Test",
                };



                context.QuestionThemes.Add(qt);

                var quest = new Questions()
                {
                    Answer = "На сцепление жми!",
                    Question = "Что должен сделать :%:?!%:№ при №*;(?№",
                    QuestionTheme = qt,
                    
                };
                context.Qustions.Add(quest);
                context.SaveChanges();
            };
            Console.WriteLine("Hello, World!");


        }
    }

}

