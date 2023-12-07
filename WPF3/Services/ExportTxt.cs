using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace WPF3.Services
{
    interface IExportTxt
    {
        public bool Export(List<string> strings);
    }
    internal class ExportTxt : IExportTxt
    {
        public bool Export(List<string> strings)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            
            if ((bool)saveFile.ShowDialog())
            {
                var path = saveFile.FileName;

                File.AppendAllLines(path, strings);

                return true;
            }

            return false;
        }
    }
}
