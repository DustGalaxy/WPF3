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
                var questions = context.Qustions.Include(p => p.QuestionTheme);

                foreach (var item in questions)
                {
                    Console.WriteLine($"{item.Question}, {item.Answer}, {item.QuestionTheme?.Name}");
                }
                questions.First().Answer = "20";
                Models.update_question(questions.First());
            }



            using (Context context = new Context())
            {
                var qe = context.Qustions.Include(p => p.QuestionTheme);

                foreach (var item in qe)
                {
                    Console.WriteLine($"{item.Question}, {item.Answer}, {item.QuestionTheme?.Name}");
                }

            }

            Console.WriteLine("Hello, World!");


        }
    }

}

