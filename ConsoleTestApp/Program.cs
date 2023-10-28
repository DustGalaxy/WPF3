// See https://aka.ms/new-console-template for more information
using WPF3.Model;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            using (Context context = new Context()) ;
        }
    }

}

