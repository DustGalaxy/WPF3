using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3.Services
{
    interface IShuffleList
    {
        extern static void ShuffleList(List<string> list);
    }



    class ShuffleListService : IShuffleList
    {
        public static void ShuffleList(List<string> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
