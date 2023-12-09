using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPF3.Infrastructure;
using WPF3.Model.Entities;
using WPF3.Services;

namespace WPF3.ViewModels
{
    internal class UserViewModel : BindableBase, INavigationAware
    {
        #region Navigate

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            listClear();
        }

        private void listClear()
        {
            MailList.Clear();
            TestList.Clear();
            ResultList.Clear();
            TimeOutList.Clear();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            user = (User)navigationContext.Parameters["user"];
            UserName = user.Name;
            dictTests = servicesTest.GetTestsDict();
            dictMail = servicesMail.GetMailsDictionary(user.UserId);
            var results = servicesResult.GetUserResults(user.UserId);
            var timeouts = servicesTimeOut.GetTimeouts(user.UserId);

            UpdateMaliList();
            UpdateResultList();
            UpdateTestList();
            UpdateTimeOutList();

        }

        #endregion

        private Services.ServicesResult servicesResult = new();
        private Services.ServiceMail servicesMail = new();
        private Services.ServiceTest servicesTest = new();
        private Services.ServiceTimeOut servicesTimeOut = new();
        private Services.ServiceUser servicesUser = new();
        public IRegionManager _regionManager;
        private User user;

        public UserViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

        }


        #region Props

        private Dictionary<string, Tests> dictTests;
        private Dictionary<string, Mail> dictMail;

        public ObservableCollection<string> MailList { get; private set; } = new();
        public ObservableCollection<string> TestList { get; private set; } = new();
        public ObservableCollection<string> ResultList { get; private set; } = new();
        public ObservableCollection<string> TimeOutList { get; private set; } = new();

        private string _selectedTest;
        public string SelectedTest
        {
            get => _selectedTest;
            set => SetProperty(ref _selectedTest, value);
        }
        

        private string _selectedMail;
        public string SelectedMail
        {
            get => _selectedMail;
            set => SetProperty(ref _selectedMail, value);
        }
        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        
        #endregion


        #region NavToLoginCommand

        private DelegateCommand _navToLoginCommand;
        public DelegateCommand NavToLoginCommand => _navToLoginCommand ??= new DelegateCommand(NavToLogin);

        private void NavToLogin()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "Login");
        }
        #endregion


        #region StartTestCommand

        private DelegateCommand _startTestCommand;
        public DelegateCommand StartTestCommand => _startTestCommand ??= new DelegateCommand(StartTest);

        private void StartTest()
        {
            if (string.IsNullOrEmpty(SelectedTest)) return;
            if (!servicesTimeOut.Time_out_check(user.UserId, dictTests[SelectedTest].Id))
            {
                MessageBox.Show("У вас наявне обмеження на проходження цього тест! Зачекайте та спробуйте пізніше");
                return;
            }

            MessageBox.Show(dictTests[SelectedTest].Name);
            NavigationParameters param = new NavigationParameters
            {
                { "test", servicesTest.GetTest(dictTests[SelectedTest].Id) },
                { "user", user}
            };
            _regionManager.RequestNavigate(Regions.ContentRegion, "Test", param);
        }
        #endregion


        #region DeleteMailCommand

        private DelegateCommand _deleteMailCommand;
        public DelegateCommand DeleteMailCommand => _deleteMailCommand ??= new DelegateCommand(DeleteMail);

        private void DeleteMail()
        {
            servicesMail.DeleteMail(dictMail[SelectedMail]);
            UpdateMaliList();
        }

        private void UpdateMaliList()
        {
            MailList.Clear();
            MailList.AddRange(dictMail.Keys.ToList());
        }
        #endregion


        #region ListUpdate

        private void UpdateResultList()
        {
            ResultList.Clear();
            foreach (var item in servicesResult.GetUserResults(user.UserId))
            {
                ResultList.Add($"\"{item.Tests.Name}\": {item.Result}");
            }
        }

        private void UpdateTestList()
        {
            TestList.Clear();
            foreach (var item in servicesTest.GetActiveTests())
            {
                TestList.Add(item.Name);
            }
        }

        private void UpdateTimeOutList()
        {
            TimeOutList.Clear();
            foreach (var item in servicesTimeOut.GetTimeouts(user.UserId))
            {
                TimeOutList.Add($"{item.Test.Name}, {item.ToUnblockDate.Kind}");
            }
        }

        #endregion


        #region ExportTxtCommand

        private DelegateCommand _exportTxtCommand;
        public DelegateCommand ExportTxtCommand => _exportTxtCommand ??= new DelegateCommand(ExportTxtExecute);

        private void ExportTxtExecute()
        {
            ExportTxt ext = new ExportTxt();
            if (ext.Export(ResultList.ToList()))
                MessageBox.Show("Результати успішно експортовані!");
        }

        #endregion
    }
}
