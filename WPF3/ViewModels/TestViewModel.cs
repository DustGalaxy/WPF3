using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPF3.Infrastructure;
using WPF3.Model.Entities;
using WPF3.Services;

namespace WPF3.ViewModels
{
    class TestViewModel : BindableBase, INavigationAware
    {
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
                
                shuffleList.ShuffleList(ref list);

                //ElementModel element = new ElementModel();
                //MessageBox.Show(element.ToString());

                Elements.Add(new ElementModel
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


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        Services.ShuffleListService shuffleList = new();
        public IRegionManager _regionManager;
        public TestViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }



        public ObservableCollection<ElementModel> Elements { get; private set; } = new();
        private Tests currTest;
        private User user;

        private DelegateCommand _endTestCommand;
        public DelegateCommand EndTestCommand => _endTestCommand ??= new DelegateCommand(EndTest);


        public void EndTest()
        {

            ServiceMail mailServices = new ServiceMail();
            ServicesResult resultServices = new ServicesResult();
            Mail mail = new Mail();
            Results res = new Results();

            res.Result = TestCheck();
            res.TestId = currTest.Id;
            res.UserId = user.UserId;
            resultServices.Create(res);

            mail.Message = $"Ваш результат за тест \"{currTest.Name}\", довівнює: {res.Result}";
            mail.UserId = user.UserId;
            mailServices.CreateMail(mail);
            MessageBox.Show("Спасибі за проходження тесту!","Тест завершенно");
            _regionManager.RequestNavigate(Regions.ContentRegion, "User", new NavigationParameters{{"userId", user.UserId}});
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
