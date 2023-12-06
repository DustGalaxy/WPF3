using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF3.Infrastructure;
using WPF3.Model.Entities;

namespace WPF3.ViewModels
{
    public class AdminViewModel : BindableBase, INavigationAware
    {


        IRegionManager _regionManager { get; set; }
        

        ObservableCollection<string> TestList = new ObservableCollection<string>();

        public AdminViewModel(IRegionManager iregionManager)
        {
            _regionManager = iregionManager;

            FillTestLIst();
        }

        private void FillTestLIst()
        {
            foreach (var i in testdict.Keys.ToList())
                TestList.Add(i);
        }

        static readonly Services.ServiceTest serviceTest = new Services.ServiceTest();
        Dictionary<string, Tests> testdict = serviceTest.GetTestsDict();

        
        

        private string _selectedtest;
        public string SelectedTest
        {
            get => _selectedtest;
            set => SetProperty(ref _selectedtest, value);
        }

        private string _errorlabel;
        public string ErrorLabel
        {
            get => _errorlabel;
            set => SetProperty(ref _errorlabel, value);
        }



        private DelegateCommand _createtestcommand;

        public DelegateCommand CreateTestCommand => _createtestcommand ??= new DelegateCommand(CreateTestExecute);

        public void CreateTestExecute()
        {

            NavigationParameters param = new()
            {
                { "Test", null }
            };
            _regionManager.RequestNavigate(Regions.ContentRegion, "CreateTest", param);
        }

        private DelegateCommand _updatetestcommand;
        public DelegateCommand UpdateTestCommand => _updatetestcommand ??= new DelegateCommand(UpdateTestExecute, IsSelectedTest);

        public void UpdateTestExecute()
        {
            if (SelectedTest != null)
            {
                NavigationParameters param = new NavigationParameters
                {
                    { "Test", SelectedTest }
                };
                _regionManager.RequestNavigate(Regions.ContentRegion, "CreateTest", param);
            }
            else
            {
                ErrorLabel = "No test selected";
            }     
        }

        private bool IsSelectedTest()
        {
            if (SelectedTest != null)
                return true;
            else 
                return false;
        }


        private DelegateCommand _deletetestcommand;
        public DelegateCommand DeleteTestCommand => _deletetestcommand ??= new DelegateCommand(DeleteTestExecute, IsSelectedTest);

        public void DeleteTestExecute()
        {
            if (SelectedTest != null)
            {
                var msg = MessageBox.Show("Confirm delete Test", "Delete Test", MessageBoxButton.OKCancel);

                if (msg == MessageBoxResult.OK)
                {
                    int id = testdict[SelectedTest].Id;

                    serviceTest.DeleteTest(id);
                }
            }
            else
            {
                ErrorLabel = "No test selected";
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            FillTestLIst();
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
