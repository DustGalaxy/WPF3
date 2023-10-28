// See https://aka.ms/new-console-template for more information
using WPF3.Model;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Context context = new Context()) ;
            Console.WriteLine("Hello, World!");
        }
    }

}

