using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using WPF3.Views;

namespace WPF3.ViewModels
{
    class MainWindowViewModel : BindableBase
    {

        public IRegionManager rmanager { get; }
        public MainWindowViewModel(IRegionManager iregionManager) 
        {
            rmanager = iregionManager;

        }


        public ObservableCollection<string> Items { get; private set; } 
            = new ObservableCollection<string>();


        private DelegateCommand _commandFunc = null;
        public DelegateCommand CommandFunc =>
            _commandFunc ??= new DelegateCommand(CommandFuncExecute);

        private void CommandFuncExecute()
        {
            Items.Add("123");
        }
        

        private string _title = "Тести для отримання водійского посвідчення";

        public string Title 
        { 
            get => _title;
            set => SetProperty<string>(ref _title, value);
        }

        private DelegateCommand _logInNav = null;
        public DelegateCommand LogInNav =>
            _logInNav ??= new DelegateCommand(CommandLoadExecute);


        private bool CanExecute()
        {
            return true;
        }

        private void CommandLoadExecute()
        {
            
        }
    }

}
