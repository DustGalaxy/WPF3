using Microsoft.EntityFrameworkCore;
using Test.Model;
using Test.Model.Entities;

namespace Test.Model
{
    class Models
    {
        public static List<Results> Get_results()
        {
            using (Context context = new Context())
            {
                return context.Results.Include(p => p.Tests).Include(p => p.User).ToList();
            }
        }

        public static Results Get_results(int userId)
        {
            using (Context context = new Context())
            {
                return context.Results.Include(p => p.Tests).Include(p => p.User).Where(p => p.UserId == userId).First();
            }
        }

        public static List<TimeOuts> Get_timeouts(int userId)
        {
            using (Context context = new Context())
            {
                return context.TimeOuts.Where(p => p.UserId == userId).ToList();
            }
        }

        public static List<Tests> Get_active_tests()
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).Where(p => p.IsActived == true).ToList();
            }
        }

        public static List<Tests> Get_tests()
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).ToList();
            }
        }

        public static List<Tests> Get_tests(int testId)
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).Where(p => p.Id == testId).ToList();
            }
        }

        public static void Create_result(Results res)
        {
            using (Context context = new Context())
            {
                context.Results.Add(res);
                context.SaveChanges();
            }
        }

        public static TimeOuts Time_out_check(int userId, int testId)
        {
            using (Context context = new Context())
            {
                return context.TimeOuts
                                       .Include(p => p.Test)
                                       .Include(p => p.User)
                                       .Where(p => p.UserId == userId)
                                       .Where(p => p.TestId == testId).First();
            }
        }

        /// <summary>
        /// Tests metods
        /// </summary>
        /// <param name="test"></param>
        public static void Create_test(Tests test)
        {
            using (Context context = new Context())
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }
        }

        public static void Update_test(Tests test)
        {
            using (Context context = new Context())
            {
                var t = context.Tests.Where(p => p.Id == test.Id).First();
                t = test;
                context.Tests.Update(t);
                context.SaveChanges();
            }
        }

        public static void Delete_test(int testId)
        {
            using (Context context = new Context())
            {
                var t = context.Tests.Where(p => p.Id == testId).First();
                context.Tests.Remove(t);
                context.SaveChanges();
            }
        }

        public static List<Questions> Get_test_questions(Tests test)
        {
            using(Context context = new Context())
            {
                return context.Qustions.Where(p => p.Tests == test).ToList();
            }
        }


        static public void Deactivate_test(Tests test)
        {
            using (Context ctx = new Context())
            {
                test.IsActived = false;
                ctx.Tests.Update(test);
                ctx.SaveChanges();
            }
        }

        static public void Activate_test(Tests test)
        {
            using (Context ctx = new Context())
            {
                test.IsActived = true;
                ctx.Tests.Update(test);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Question metods
        /// </summary>
        /// <param name="question"></param>
        
        public static void Create_question(Questions question)
        {
            using (Context context = new Context())
            {
                context.Qustions.Add(question);
                context.SaveChanges();
            }
        }

        public static void Update_question(Questions question)
        {
            using (Context context = new Context())
            {
                context.Qustions.Update(question);
                context.SaveChanges();
            }
        }

        public static void Delete_question(Questions question)
        {
            using (Context context = new Context())
            {
                context.Qustions.Remove(question);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Themes metods
        /// </summary>
        /// <param name="theme"></param>
        public static void Create_qustion_theme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Add(theme);
                context.SaveChanges();
            }
        }

        public static void Update_qustion_theme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Update(theme);
                context.SaveChanges();
            }
        }

        public static void Delete_qustion_theme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Remove(theme);
                context.SaveChanges();
            }
        }

        public static List<QuestionTheme> Get_themes_list()
        {
            using (Context context = new Context())
            {
                return context.QuestionThemes.ToList();
            }
        }

        

    }


}
