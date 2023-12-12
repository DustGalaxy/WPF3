using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPF3.Infrastructure;
using WPF3.Model.Entities;

namespace WPF3.ViewModels
{
    public class AdminViewModel : BindableBase, INavigationAware
    {
        #region Navigate

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdTestList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion


        IRegionManager _regionManager { get; set; }
        static readonly Services.ServiceTest serviceTest = new Services.ServiceTest();

        public AdminViewModel(IRegionManager iregionManager)
        {
            _regionManager = iregionManager;
            UpdTestList();
        }

        #region Update list

        private void FillTestLIst()
        {
            testdict = serviceTest.GetTestsDict();
            foreach (var i in testdict.Keys.ToList())
                TestList.Add(i);
        }

        public void UpdTestList()
        {
            TestList.Clear();
            FillTestLIst();
        }

        #endregion


        #region Props

        public ObservableCollection<string> TestList { get; private set; } = new ObservableCollection<string>();
        Dictionary<string, Tests> testdict;

        private string _selectedtest = null;
        public string SelectedTest
        {
            get => _selectedtest;
            set => SetProperty<string>(ref _selectedtest, value);
        }

        private string _errorlabel;
        public string ErrorLabel
        {
            get => _errorlabel;
            set => SetProperty(ref _errorlabel, value);
        }

        #endregion


        #region CreateTestCommand

        private DelegateCommand _createtestcommand;

        public DelegateCommand CreateTestCommand =>
            _createtestcommand ??= new DelegateCommand(CreateTestExecute);

        public void CreateTestExecute()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "CreateTest");
        }

        #endregion


        #region NavToLoginCommand

        private DelegateCommand _navToLoginCommand;
        public DelegateCommand NavToLoginCommand =>
            _navToLoginCommand ??= new DelegateCommand(NavToLogin);

        public void NavToLogin()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "Login");
        }

        #endregion


        #region DeleteTestCommand

        private DelegateCommand _deletetestcommand;
        public DelegateCommand DeleteTestCommand => _deletetestcommand ??= new DelegateCommand(DeleteTestExecute);

        public void DeleteTestExecute()
        {
            if (!string.IsNullOrEmpty(SelectedTest))
            {
                var msg = MessageBox.Show("Вивпевненні що хочете видалити тест?", "Видалення тесту", MessageBoxButton.OKCancel);

                if (msg == MessageBoxResult.OK)
                {
                    int id = testdict[SelectedTest].Id;

                    serviceTest.DeleteTest(id);
                    UpdTestList();
                }
            }
            else
            {
                MessageBox.Show("Оберіть тест");
                ErrorLabel = "No test selected";
            }
        }

        #endregion

    }
}
