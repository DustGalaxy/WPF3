// See https://aka.ms/new-console-template for more information

using Test.Model;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ExportTxt export = new ExportTxt();
            export.Export(
                "F:\\CSharp\\WPFapp1\\WPF3\\ConsoleTestApp\\TextFile1.txt",
                new List<string>
                {
                    "Нова сторока 1",
                    "Нова сторока 2",
                    "Нова сторока 3",
                    "Нова сторока 4",
                    "Нова сторока 5",
                });

        }
    }

}

