﻿using ConsoleTestApp.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Test.Model;
using Test.Model.Entities;

// Програмне забезпечення проведення іспитів на отримання водійського посвідчення.
// Адміністратор може визначати перелік тестових запитань
// (з зображеннями в тому числі), відповідей та правильні відповіді на кожне запитання.
// Звичайні користувачі можуть здавати  іспит з обмеженнями в часі.
// Набір запитань для кожного тесту формується випадковим чином.
// У результаті іспиту користувач має отримати повідомлення про те, чи успішно він склав іспит, та свій загальний результат.
// Для кожного користувача мають зберігатися результати  його іспитів.
// Менеджери можуть виконувати пошук користувачів та переглядати їхні результати.
// У випадку, якщо кількість невдалих  спроб перевищила встановлений ліміт,
// доступ до тестів має бути для  нього заблокований на деякий час. 


namespace Test.Model
{
    class Models
    {
        /// <summary>
        /// Select metods
        /// </summary>

        public static List<Results> GetResults()
        {
            using (Context context = new Context())
            {
                return context.Results.Include(p => p.Tests).Include(p => p.User).ToList();
            }
        }

        public static Results GetResultsByUser(int userId)
        {
            using (Context context = new Context())
            {
                return context.Results.Include(p => p.Tests).Include(p => p.User).Where(p => p.UserId == userId).First();
            }
        }

        public static List<TimeOuts> GetTimeouts(int userId)
        {
            using (Context context = new Context())
            {
                return context.TimeOuts.Where(p => p.UserId == userId).ToList();
            }
        }

        public static List<Tests> GetActiveTests()
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).Where(p => p.IsActived == true).ToList();
            }
        }

        public static List<Tests> GetTests()
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).ToList();
            }
        }

        public static Tests GetTest(int testId)
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).Where(p => p.Id == testId).First();
            }
        }

        public static void CreateResult(Results res)
        {
            using (Context context = new Context())
            {
                context.Results.Add(res);
                context.SaveChanges();
            }
        }

        public static void CreateTimeOut(User user, Tests test, DateTime dateTime)
        {
            using (Context context = new Context())
            {
                var timeout = new TimeOuts()
                {
                    User = user,
                    Test = test,
                    ToUnblockDate = dateTime
                };
                context.TimeOuts.Add(timeout);
                context.SaveChanges();
            }
        }

        public static TimeOuts Time_out_check(int userId, int testId)
        {
            using (Context context = new Context())
            {
                return context.TimeOuts.Include(p => p.Test)
                                       .Include(p => p.User)
                                       .Where(p => p.UserId == userId)
                                       .Where(p => p.TestId == testId).First();
            }
        }

        /// <summary>
        /// Tests metods
        /// </summary>
        public static void CreateTest(Tests test)
        {
            using (Context context = new Context())
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }
        }



        public static void UpdateTest(Tests test)
        {
            using (Context context = new Context())
            {
                context.Tests.Update(test);
                context.SaveChanges();
            }
        }

        public static void DeleteTest(int testId)
        {
            using (Context context = new Context())
            {
                var t = context.Tests.Where(p => p.Id == testId).First();
                context.Tests.Remove(t);
                context.SaveChanges();
            }
        }

        public static List<Questions> GetTestQuestions(Tests test)
        {
            using(Context context = new Context())
            {
                return context.Qustions.Where(p => p.Tests == test).ToList();
            }
        }


        static public void DeactivateTest(Tests test)
        {
            using (Context ctx = new Context())
            {
                test.IsActived = false;
                ctx.Tests.Update(test);
                ctx.SaveChanges();
            }
        }

        static public void ActivateTest(Tests test)
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

        
        public static void CreateQuestion(Questions question)
        {
            using (Context context = new Context())
            {
                context.Qustions.Add(question);
                context.SaveChanges();
            }
        }

        public static void UpdateQuestion(Questions question)
        {
            using (Context context = new Context())
            {
                context.Qustions.Update(question);
                context.SaveChanges();
            }
        }

        public static void DeleteQuestion(Questions question)
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
        public static void CreateQustionTheme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Add(theme);
                context.SaveChanges();
            }
        }

        public static void UpdateQustionTheme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Update(theme);
                context.SaveChanges();
            }
        }

        public static void DeleteQustionTheme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Remove(theme);
                context.SaveChanges();
            }
        }

        public static List<QuestionTheme> GetThemesList()
        {
            using (Context context = new Context())
            {
                return context.QuestionThemes.ToList();
            }
        }

        /// <summary>
        /// User metods
        /// </summary>

        public static int LoginUser(string email, string password)
        {
            using (Context context = new Context())
            {
                if (!ValidateUser(email))
                {
                    var user = context.Users.Where(p => p.Email == email && p.Password == PassHasher.Hash(password)).First();
                    return user.UserType;
                }
                throw new Exception("User dosn`t exist");
            }
        }

        public static bool ValidateUser(string email)
        {
            using( Context context = new Context())
            {
                try
                {
                    context.Users.Where(p => p.Email == email).First();
                    return false;
                }
                catch (Exception ex)
                {
                    return true;
                }
            }
        }

        public static int CreateUser(User user)
        {
            if (ValidateUser(user.Email))
            {
                return 0;
            }

            using (Context context = new Context())
            {
                user.Password = PassHasher.Hash(user.Password);
                context.Users.Add(user);
                context.SaveChanges();
                return 1;
            }
        }

        public static void UpdateUser(User user)
        {
            using (Context context = new Context())
            {
                context.Users.Update(user);
                context.SaveChanges();
            }
        }

        public static void DeleteUser(User user)
        {
            using (Context context = new Context())
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public static User GetUser(int Id)
        {
            using (Context context = new Context())
            {
                return context.Users.Where(p => p.UserId == Id).Single();
            }
        }


        public static List<User> GetUsersByType(int type)
        {
            using (Context context = new Context())
            {
                return context.Users.Where(p => p.UserType == type).ToList();
            }
        }



        ///
        public static void CreateMail(Mail mail)
        {
            using (Context context = new Context())
            {
                context.Mails.Add(mail);
                context.SaveChanges();
            }
        }

        public static void UpdateMail(Mail mail)
        {
            using (Context context = new Context())
            {
                context.Mails.Update(mail);
                context.SaveChanges();
            }
        }

        public static void DeleteMail(Mail mail)
        {
            using (Context context = new Context())
            {
                context.Mails.Remove(mail);
                context.SaveChanges();
            }
        }



        public static void CreateLog(int userId, string filePath)
        {

            try
            {
                List<string> strings = new List<string>();
                using (Context context = new Context())
                {
                    var res = context.Results.Where(p => p.UserId == userId).ToList();
                    foreach (var i in res)
                    {
                        strings.Add(i.Result);
                    }
                    
                }

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    for (int i = 0; i < strings.Count(); ++i)
                    {
                        sw.WriteLine(strings[i]);
                    }
                }

                Console.WriteLine($"Операція виконана успішно. Результат записано в {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }


    }


}
