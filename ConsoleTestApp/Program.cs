// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Test.Model;
using Test.Model.Entities;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //QuestionTheme questionTheme = new QuestionTheme()
            //{
            //    Name = "Перша тема",
            //    Description = "test",
            //};
            //Models.create_qustion_theme(questionTheme);
            //using (Context context = new Context())
            //{
            //    var questionTheme = new QuestionTheme()
            //    {
            //        Name = "Друга тема",
            //        Description = "test",
            //    };

            //    Questions question = new Questions()
            //    {
            //        Answer = "5",
            //        Question = "2+3",
            //        QuestionTheme = questionTheme,
            //        ImageSrc = "",
            //        WAns1 = "4",
            //        WAns2 = "7",
            //        WAns3 = "-1",
            //    };
            //    Models.create_qustion(question);
            //}



            using (Context context = new Context())
            {

                var user = new User()
                {
                    Name = "Test",
                    Surname = "Test",
                    Email = "Test",
                    Password = "Test",
                    UserType = 1
                };

                context.Users.Add(user);
            }



            //var user2 = new User()
            //{
            //    Name = "Test2",
            //    Surname = "Test2",
            //    Email = "Test2",
            //    Password = "Test2",
            //    UserType = 1
            //};

            //string str = $"{Models.ValidateUser(user.Email)}, {Models.ValidateUser(user2.Email)}";
            //Console.WriteLine(str);


        }
    }

}

