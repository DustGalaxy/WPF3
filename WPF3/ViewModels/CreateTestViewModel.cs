using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF3.Infrastructure;

namespace WPF3.ViewModels
{
    internal class CreateTestViewModel : BindableBase, INavigationAware
    {

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public IRegionManager _regionManager { get; private set; }

        public CreateTestViewModel(IRegionManager regionManager)
        { 
            _regionManager = regionManager;

        }



        public ObservableCollection<String> QuestionListFrom { get; private set; } = new ObservableCollection<String>();
        public ObservableCollection<String> QuestionListTo { get; private set; } = new ObservableCollection<String>();


        private string _selectedLeft = null;
        public string SelectedLeft
        {
            get => _selectedLeft;
            set
            {
                if (SetProperty<string>(ref _selectedLeft, value))
                {
                    Debug.WriteLine(_selectedLeft ?? "no customer selected");
                }
            }
        }

        private string _selectedRight = null;
        public string SelectedRight
        {
            get => _selectedRight;
            set
            {
                if (SetProperty<string>(ref _selectedRight, value))
                {
                    Debug.WriteLine(_selectedRight ?? "no customer selected");
                }
            }
        }

        private int counter = 0;

        private DelegateCommand _commandLoad = null;
        public DelegateCommand CommandLoad =>
            _commandLoad ?? (_commandLoad = new DelegateCommand(CommandLoadExecute));

        private void CommandLoadExecute()
        {
            counter++;
            QuestionListFrom.Add($"{counter}");
        }

        private DelegateCommand _moverightcommand = null;
        public DelegateCommand MoveRightCommand =>
            _moverightcommand ?? (_moverightcommand = new DelegateCommand(MoveRightExecute));

        public void MoveRightExecute()
        {
            if (SelectedLeft == null) return;
            QuestionListTo.Add(SelectedLeft);
            QuestionListFrom.Remove(SelectedLeft);
        }


        private DelegateCommand _moveleftcommand = null;
        public DelegateCommand MoveLeftCommand =>
            _moveleftcommand ?? (_moveleftcommand = new DelegateCommand(MoveLeftExecute));


        public void MoveLeftExecute()
        {
            if (SelectedRight == null) return;
            QuestionListFrom.Add(SelectedRight);
            QuestionListTo.Remove(SelectedRight);
        }


        private DelegateCommand _createnewquestioncommand = null;
        public DelegateCommand CreateNewQuestionCommand =>
            _createnewquestioncommand ?? (_createnewquestioncommand = new DelegateCommand(CreateNewQuestion));
        
        public void CreateNewQuestion()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "CreateQuestion");
        }


    }
}
