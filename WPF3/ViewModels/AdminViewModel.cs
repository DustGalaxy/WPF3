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


        IRegionManager _regionManager { get; set; }
        

        public ObservableCollection<string> TestList { get; private set; } = new ObservableCollection<string>();

        public AdminViewModel(IRegionManager iregionManager)
        {
            _regionManager = iregionManager;
            UpdTestList();
        }

        private void FillTestLIst()
        {
            // MessageBox.Show($"{testdict.Keys.ToList().FirstOrDefault("no")}");
            foreach (var i in testdict.Keys.ToList())
                TestList.Add(i);
        }

        static readonly Services.ServiceTest serviceTest = new Services.ServiceTest();
        Dictionary<string, Tests> testdict = serviceTest.GetTestsDict();

        
        

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



        private DelegateCommand _createtestcommand;

        public DelegateCommand CreateTestCommand => 
            _createtestcommand ??= new DelegateCommand(CreateTestExecute);

        public void CreateTestExecute()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "CreateTest");
        }


        private DelegateCommand _navToLoginCommand;
        public DelegateCommand NavToLoginCommand =>
            _navToLoginCommand ??= new DelegateCommand(NavToLogin);

        public void NavToLogin()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "Login");
        }

        private DelegateCommand _deletetestcommand;
        public DelegateCommand DeleteTestCommand => _deletetestcommand ??= new DelegateCommand(DeleteTestExecute);

        public void DeleteTestExecute()
        {
            if (SelectedTest != null)
            {
                var msg = MessageBox.Show("Confirm delete Test", "Delete Test", MessageBoxButton.OKCancel);

                if (msg == MessageBoxResult.OK)
                {
                    int id = testdict[SelectedTest].Id;

                    serviceTest.DeleteTest(id);
                    UpdTestList();
                }
            }
            else
            {
                MessageBox.Show("No test selected");
                ErrorLabel = "No test selected";
            }
        }

        public void UpdTestList()
        {
            TestList.Clear();
            FillTestLIst();
        }


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
    }
}
