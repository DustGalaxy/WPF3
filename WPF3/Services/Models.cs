using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using WPF3.Model;
using WPF3.Model.Entities;

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


namespace WPF3.Services
{
    interface IDataResult
    {
        public void Create(Results res);
        public List<Results> GetResults();
        public Results GetResultsByUser(int userId);

    }

    class ServicesResult : IDataResult
    {

        public void Create(Results res)
        {
            using (Context context = new Context())
            {
                context.Results.Add(res);
                context.SaveChanges();
            }
        }

        public List<Results> GetResults()
        {
            using (Context context = new Context())
            {
                return context.Results.Include(p => p.Tests).Include(p => p.User).ToList();
            }
        }

        public Results GetResultsByUser(int userId)
        {
            using (Context context = new Context())
            {
                return context.Results.Include(p => p.Tests).Include(p => p.User).Where(p => p.UserId == userId).First();
            }
        }
    }


    interface IDataTimeOut
    {
        public List<TimeOuts> GetTimeouts(int userId);
        public void CreateTimeOut(User user, Tests test, DateTime dateTime);
        public TimeOuts Time_out_check(int userId, int testId);
    }
    
    class ServiceTimeOut : IDataTimeOut
    {
        public List<TimeOuts> GetTimeouts(int userId)
        {
            using (Context context = new Context())
            {
                return context.TimeOuts.Where(p => p.UserId == userId).ToList();
            }
        }

        public void CreateTimeOut(User user, Tests test, DateTime dateTime)
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

        public TimeOuts Time_out_check(int userId, int testId)
        {
            using (Context context = new Context())
            {
                return context.TimeOuts.Include(p => p.Test)
                                       .Include(p => p.User)
                                       .Where(p => p.UserId == userId)
                                       .Where(p => p.TestId == testId).First();
            }
        }
    }


    interface IDataTest
    {
        public List<Tests> GetActiveTests();
        public List<Tests> GetTests();
        public Tests GetTest(int testId);
        public void CreateTest(Tests test);
        public void UpdateTest(Tests test);
        public void DeleteTest(int testId);
        public List<Questions> GetTestQuestions(Tests test);
        public void DeactivateTest(Tests test);
        public void ActivateTest(Tests test);
    }

    class ServiceTest : IDataTest
    {
        public List<Tests> GetActiveTests()
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).Where(p => p.IsActived == true).ToList();
            }
        }

        public List<Tests> GetTests()
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).ToList();
            }
        }

        public Tests GetTest(int testId)
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions)
                                    .Where(p => p.Id == testId).First();
            }
        }

        public void CreateTest(Tests test)
        {
            using (Context context = new Context())
            {
                context.Tests.Add(test);
                context.SaveChanges();
            }
        }



        public void UpdateTest(Tests test)
        {
            using (Context context = new Context())
            {
                context.Tests.Update(test);
                context.SaveChanges();
            }
        }

        public void DeleteTest(int testId)
        {
            using (Context context = new Context())
            {
                var t = context.Tests.Where(p => p.Id == testId).First();
                context.Tests.Remove(t);
                context.SaveChanges();
            }
        }

        public List<Questions> GetTestQuestions(Tests test)
        {
            using (Context context = new Context())
            {
                return context.Qustions.Where(p => p.Tests == test).ToList();
            }
        }


        public void DeactivateTest(Tests test)
        {
            using (Context ctx = new Context())
            {
                test.IsActived = false;
                ctx.Tests.Update(test);
                ctx.SaveChanges();
            }
        }

        public void ActivateTest(Tests test)
        {
            using (Context ctx = new Context())
            {
                test.IsActived = true;
                ctx.Tests.Update(test);
                ctx.SaveChanges();
            }
        }

        public Dictionary<string, Tests> GetTestsDict()
        {
            var tests = GetTests();
            Dictionary<string, Tests> pairs = new Dictionary<string, Tests>();
            foreach (var item in tests)
            {
                pairs.Add(item.Name, item);
            }
            return pairs;
        }

    }


    interface IDataQuestion
    {
        public void CreateQuestion(Questions question);
        public void UpdateQuestion(Questions question);
        public void DeleteQuestion(Questions question);
    }

    class ServiceQuestion : IDataQuestion
    {
        public void CreateQuestion(Questions question)
        {
            using (Context context = new Context())
            {
                context.Qustions.Add(question);
                context.SaveChanges();
            }
        }

        public void UpdateQuestion(Questions question)
        {
            using (Context context = new Context())
            {
                context.Qustions.Update(question);
                context.SaveChanges();
            }
        }

        public void DeleteQuestion(Questions question)
        {
            using (Context context = new Context())
            {
                context.Qustions.Remove(question);
                context.SaveChanges();
            }
        }
    }


    interface IDataQuestionTheme
    {
        public void CreateQustionTheme(QuestionTheme theme);
        public void UpdateQustionTheme(QuestionTheme theme);
        public void DeleteQustionTheme(QuestionTheme theme);
        public List<QuestionTheme> GetThemesList();
    }

    class ServiceQuestionTheme : IDataQuestionTheme
    {
        public void CreateQustionTheme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Add(theme);
                context.SaveChanges();
            }
        }

        public void UpdateQustionTheme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Update(theme);
                context.SaveChanges();
            }
        }

        public void DeleteQustionTheme(QuestionTheme theme)
        {
            using (Context context = new Context())
            {
                context.QuestionThemes.Remove(theme);
                context.SaveChanges();
            }
        }

        public List<QuestionTheme> GetThemesList()
        {
            using (Context context = new Context())
            {
                return context.QuestionThemes.ToList();
            }
        }
    }


    interface IDataUser
    {
        public User LoginUser(string email, SecureString password);
        public bool ValidateUser(string email);
        public int CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        public User GetUser(int Id);
        public List<User> GetUsersByType(int type);

    }

    class ServiceUser : IDataUser
    {
        public User LoginUser(string email, SecureString password)
        {
            using (Context context = new Context())
            {
                if (!ValidateUser(email))
                {
                    var user = context.Users.Where(p => p.Email == email && p.Password == PassHasher.GetSecureHash(password)).First();
                    return user;
                }
                throw new Exception("User dosn`t exist");
            }
        }

        public bool ValidateUser(string email)
        {
            using (Context context = new Context())
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

        public int CreateUser(User user)
        {
            if (!ValidateUser(user.Email))
            {
                return 0;
            }
            Debug.WriteLine("123");
            using (Context context = new Context())
            {
                SecureString secure = new SecureString();

                foreach (char i in user.Password.ToCharArray())
                    secure.AppendChar(i);
                user.Password = PassHasher.GetSecureHash(secure);
                context.Users.Add(user);
                context.SaveChanges();
                return 1;
            }
        }

        public void UpdateUser(User user)
        {
            using (Context context = new Context())
            {
                context.Users.Update(user);
                context.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            using (Context context = new Context())
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public User GetUser(int Id)
        {
            using (Context context = new Context())
            {
                return context.Users.Where(p => p.UserId == Id).Single();
            }
        }


        public List<User> GetUsersByType(int type)
        {
            using (Context context = new Context())
            {
                return context.Users.Where(p => p.UserType == type).ToList();
            }
        }
    }


    interface IDataMail
    {
        public void CreateMail(Mail mail);
        public void UpdateMail(Mail mail);
        public void DeleteMail(Mail mail);
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
    }




    class Models
    {
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
