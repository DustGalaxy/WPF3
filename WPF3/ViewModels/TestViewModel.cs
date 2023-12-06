using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPF3.Model.Entities;
using WPF3.Services;

namespace WPF3.ViewModels
{
    internal class TestViewModel : BindableBase, INavigationAware
    {
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            currTest = (Tests)navigationContext.Parameters["test"];
            user = (User)navigationContext.Parameters["user"];
            foreach (var item in currTest.Questions)
            {

                List<string> list = new List<string>
                {
                    item.Answer,
                    item.WAns1,
                    item.WAns2,
                    item.WAns3
                };

                Services.ShuffleListService.ShuffleList(list);

                ElementModel element = new ElementModel();
                MessageBox.Show(element.ToString());

                _elementService.AddElement(new ElementModel
                {
                    ImagePath = item.ImageSrc,
                    LabelText1 = item.QuestionTheme.Name,
                    LabelText2 = item.Question,
                    RadioButton1 = list[0],
                    RadioButton2 = list[1],
                    RadioButton3 = list[2],
                    RadioButton4 = list[3],
                    IsRadioButton1Checked = false,
                    IsRadioButton2Checked = false,
                    IsRadioButton3Checked = false,
                    IsRadioButton4Checked = false,
                    answer = item.Answer,
                });
            }

        }

        private readonly IElementService _elementService;
        private readonly IRegionManager _regionManager;

        public ObservableCollection<ElementModel> Elements => _elementService.GetElements();
        private Tests currTest;
        private User user;

        public TestViewModel(IElementService elementService, IRegionManager regionManager)
        {
            _elementService = elementService;
            _regionManager = regionManager;
        }

        private DelegateCommand _endtestcommand;
        public DelegateCommand Endtestcommand => _endtestcommand ??= new DelegateCommand(EndTest);


        public void EndTest()
        {

            ServiceMail mailServices = new ServiceMail();
            Mail mail = new Mail();

            mail.Message = $"Ваш результат за тест \"{currTest.Name}\", довівнює: {TestCheck()}";
            mail.UserId = user.UserId;
            mailServices.CreateMail(mail);
        }

        public string TestCheck()
        {
            int res = 0;
            int count = 0;
            foreach (var item in Elements)
            {
                if (item.IsRadioButton1Checked && item.RadioButton1 == item.answer)
                {
                    res++;
                }
                else if (item.IsRadioButton2Checked && item.RadioButton2 == item.answer)
                {
                    res++;
                }
                else if (item.IsRadioButton3Checked && item.RadioButton3 == item.answer)
                {
                    res++;
                }
                else if (item.IsRadioButton4Checked && item.RadioButton4 == item.answer)
                {
                    res++;
                }
                count++;
            }
            return $"{res}/{count}";
        }
    }
}
