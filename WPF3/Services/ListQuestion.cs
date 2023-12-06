using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3.Services
{
    public class ElementModel
    {
        public string ImagePath { get; set; }
        public string LabelText1 { get; set; }
        public string LabelText2 { get; set; }
        public string RadioButton1 { get; set; }
        public string RadioButton2 { get; set; }
        public string RadioButton3 { get; set; }
        public string RadioButton4 { get; set; }
        public bool IsRadioButton1Checked { get; set; }
        public bool IsRadioButton2Checked { get; set; }
        public bool IsRadioButton3Checked { get; set; }
        public bool IsRadioButton4Checked { get; set; }
        public string answer {  get; set; }
    }

    public interface IElementService
    {
        ObservableCollection<ElementModel> GetElements();
        void AddElement(ElementModel element);
    }

    public class ElementService : IElementService
    {
        private readonly ObservableCollection<ElementModel> _elements = new ObservableCollection<ElementModel>();

        public ObservableCollection<ElementModel> GetElements()
        {
            return _elements;
        }

        public void AddElement(ElementModel element)
        {
            _elements.Add(element);
        }


    }
}
