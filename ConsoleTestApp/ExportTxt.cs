
using System.Diagnostics;

namespace Test.Model
{
    interface IExportTxt
    {
        public bool Export(string path, List<string> strings);
    }
    internal class ExportTxt : IExportTxt
    {
        public bool Export(string path, List<string> strings)
        {
            try
            {
                File.AppendAllLines(path, strings);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
            
        }
    }
}
