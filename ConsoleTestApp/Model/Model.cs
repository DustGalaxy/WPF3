using ConsoleTestApp.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF3.Model.Entities;

namespace WPF3.Model
{
    class Model
    {
        public List<Results> get_results() 
        { 
            using( Context context = new Context())
            { 
                return context.Results.Include(p => p.Tests).Include(p => p.User).ToList();
            } 
        }
        public Results get_results(int userId)
        {
            using (Context context = new Context())
            {
                return context.Results.Include(p => p.Tests).Include(p => p.User).Where(p => p.UserId == userId).First();
            }
        }

        public List<TimeOuts> get_timeouts(int userId)
        {
            using (Context context = new Context())
            {
                return context.TimeOuts.Where(p => p.UserId == userId).ToList();
            }
        }

        public List<Tests> get_tests()
        {
            using (Context context = new Context())
            {
                return context.Tests.Include(p => p.Questions).Where(p => p.IsActived == true).ToList();
            }
        }
    }
}
