using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using WPF3.Model;
using WPF3.Model.Entities;

namespace WPF3.Services
{
    interface IDataResult
    {
        public void Create(Results res);
        public List<Results> GetResults();
        public List<Results> GetUserResults(int userId);

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

        public List<Results> GetUserResults(int userId)
        {
            using (Context context = new Context())
            {
                return context.Results.Include(p => p.Tests).Include(p => p.User).Where(p => p.UserId == userId).ToList();
            }
        }
    }


    interface IDataTimeOut
    {
        public List<TimeOuts> GetTimeouts(int userId);
        public void CreateTimeOut(User user, Tests test, DateTime dateTime);
        public bool Time_out_check(int userId, int testId);
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

        public bool Time_out_check(int userId, int testId)
        {
            using (Context context = new Context())
            {
                
                try
                {
                   var timeout = context.TimeOuts
                        .Where(p => p.UserId == userId)
                        .Where(p => p.TestId == testId)
                        .Where(p => p.ToUnblockDate > DateTime.Now).First();
                }
                catch (Exception e)
                {
                    return true;

                }

                return false;
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
                                    .ThenInclude(p => p.QuestionTheme)
                                    .Where(p => p.Id == testId)
                                    .First();
            }
        }

        public void CreateTest(Tests test)
        {
            using (Context context = new Context())
            {
                test.Questions = context.Qustions.Where(p => test.QuestionsId.Contains(p.Id)).ToList();
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
                try
                {
                    return context.Tests.Include(p => p.Questions).Where(p => p.Id == test.Id).First().Questions;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    Debug.WriteLine("Test does`t exist");
                    return new List<Questions>();
                }
                
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
        public List<Questions> GetQuestionsList();
        public Dictionary<string, Questions> GetQuestionDictionary();
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

        public List<Questions> GetQuestionsList()
        {
            using (Context context = new Context())
            {
                return context.Qustions.ToList();
            }
        }

        public Dictionary<string, Questions> GetQuestionDictionary()
        {
            var questions = GetQuestionsList();
            Dictionary<string, Questions> pairs = new Dictionary<string, Questions>();
            foreach (var item in questions)
            {
                pairs.Add(item.Name, item);
            }
            return pairs;
        }
    }


    interface IDataQuestionTheme
    {
        public void CreateQustionTheme(QuestionTheme theme);
        public void UpdateQustionTheme(QuestionTheme theme);
        public void DeleteQustionTheme(QuestionTheme theme);
        public List<QuestionTheme> GetThemesList();
        public Dictionary<string, QuestionTheme> GetThemeDictionary();
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

        public Dictionary<string, QuestionTheme> GetThemeDictionary()
        {
            var questionThemes = GetThemesList();
            Dictionary<string, QuestionTheme> pairs = new Dictionary<string, QuestionTheme>();
            foreach (var item in questionThemes)
            {
                pairs.Add(item.Name, item);
            }
            return pairs;
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
        public Dictionary<string, User> GetUsersDictionary();

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

        public List<User> GetUsersList()
        {
            using (Context context = new Context())
            {
                return context.Users.ToList();
            }
        }

        public Dictionary<string, User> GetUsersDictionary()
        {
            using (Context context = new Context())
            {
                var questions = GetUsersList();
                Dictionary<string, User> pairs = new Dictionary<string, User>();
                foreach (var item in questions)
                {
                    pairs.Add(item.Name, item);
                }
                return pairs;
            }
        }
    }


    interface IDataMail
    {
        public void CreateMail(Mail mail);
        public void UpdateMail(Mail mail);
        public void DeleteMail(Mail mail);
        public Dictionary<string, Mail> GetMailsDictionary(int userId);
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

        public Dictionary<string, Mail> GetMailsDictionary(int userId)
        {
            List<Mail> mails = new List<Mail>();
            using (Context context = new Context())
            {
                mails = context.Mails.Where(p => p.UserId == userId).ToList();
            }
            Dictionary<string, Mail> pairs = new Dictionary<string, Mail>();
            foreach (var item in mails)
            {
                pairs.Add(item.Message, item);
            }
            return pairs;
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
