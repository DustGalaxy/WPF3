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
using System.Windows;
using WPF3.Infrastructure;
using WPF3.Model.Entities;
using WPF3.Services;

namespace WPF3.ViewModels
{
    internal class CreateTestViewModel : BindableBase, INavigationAware
    {
        #region Navigate

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            QuestionListFrom.Clear();
            QuestionListTo.Clear();
        }

        private DelegateCommand _navToAdminCommand = null;
        public DelegateCommand NavToAdminCommand =>
            _navToAdminCommand ??= new DelegateCommand(NavToAdmin);

        public void NavToAdmin()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "Admin");
        }

        #endregion

        public IRegionManager _regionManager { get; private set; }
        private Services.ServiceQuestion dataQuestion;
        
        public CreateTestViewModel(IRegionManager regionManager)
        { 
            _regionManager = regionManager;
            dataQuestion = new Services.ServiceQuestion();
        }


        #region Prop`s

        public ObservableCollection<string> QuestionListFrom { get; private set; } = new ObservableCollection<string>();
        public ObservableCollection<string> QuestionListTo { get; private set; } = new ObservableCollection<string>();
        private Dictionary<string, Questions> dictQuestions = new Dictionary<string, Questions>();


        private string _selectedFrom = null;
        public string SelectedFrom
        {
            get => _selectedFrom;
            set => SetProperty<string>(ref _selectedFrom, value);
        }

        private string _selectedTo = null;
        public string SelectedTo
        {
            get => _selectedTo;
            set => SetProperty<string>(ref _selectedTo, value);
        }


        private string _nameTest = "Назва тест";
        public string NameTest
        {
            get => _nameTest;
            set => SetProperty<string>(ref _nameTest, value);
        }
        private string _descriptionTest = "Опис тесту";
        public string DescriptionTest
        {
            get => _descriptionTest;
            set => SetProperty<string>(ref _descriptionTest, value);
        }

        private int _questCount = 1;
        public int NumericValue
        {
            get => _questCount;
            set => SetProperty<int>(ref _questCount, value);
        }

        private int _maxNumValue = 1;

        public int MaxNumValue
        {
            get => _maxNumValue;
            set => SetProperty<int>(ref _maxNumValue, value);
        }

        private bool _isActive = true;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty<bool>(ref _isActive, value);
        }

        #endregion


        #region MoveCommand

        private DelegateCommand _moverightcommand = null;
        public DelegateCommand MoveRightCommand =>
            _moverightcommand ?? (_moverightcommand = new DelegateCommand(MoveRightExecute));

        public void MoveRightExecute()
        {
            if (SelectedFrom == null) return;
            QuestionListTo.Add(SelectedFrom);
            QuestionListFrom.Remove(SelectedFrom);
            MaxNumValue = QuestionListTo.Count;
        }


        private DelegateCommand _moveleftcommand = null;
        public DelegateCommand MoveLeftCommand =>
            _moveleftcommand ?? (_moveleftcommand = new DelegateCommand(MoveLeftExecute));


        public void MoveLeftExecute()
        {
            if (SelectedTo == null) return;
            QuestionListFrom.Add(SelectedTo);
            QuestionListTo.Remove(SelectedTo);
            MaxNumValue = QuestionListTo.Count;
        }

        #endregion


        #region CreateNewQuestionCommand

        private DelegateCommand _createnewquestioncommand = null;
        public DelegateCommand CreateNewQuestionCommand =>
            _createnewquestioncommand ??= new DelegateCommand(CreateNewQuestion);

        public void CreateNewQuestion()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "CreateQuestion");
        }

        #endregion


        #region CreateTestCommand

        private DelegateCommand _createTestCommand = null;
        public DelegateCommand CreateTestCommand =>
            _createTestCommand ??= new DelegateCommand(CreateTest);

        private void CreateTest()
        {
            Tests test = new Tests
            {
                Name = NameTest,
                Count = NumericValue,
                Description = DescriptionTest,
                IsActived = IsActive,
                QuestionsId = get_quests_from_list(),
            };

            Services.ServiceTest serviceTest = new ServiceTest();
            serviceTest.CreateTest(test);
            MessageBox.Show("Тест успішно додано! Оновлюємо списки...");

        }

        private List<int> get_quests_from_list()
        {
            List<int> q = new List<int>();
            foreach (var item in QuestionListTo)
            {
                q.Add(dictQuestions[item].Id);
                Debug.WriteLine(dictQuestions[item].Name, dictQuestions[item].Tests.Count);
            }
            
            return q;
        }

        #endregion


        #region DeleteQuestionCommand

        private DelegateCommand _deleteQuestionCommand = null;
        public DelegateCommand DeleteQuestionCommand =>
            _deleteQuestionCommand ??= new DelegateCommand(DeleteQuestion);

        private void DeleteQuestion()
        {
            Services.ServiceQuestion serviceQ = new ServiceQuestion();
            if (SelectedFrom is not null)
            {
                var msgRes = MessageBox.Show("Ви дійсно бажаєте видалити питання", "Видалення питання",
                    MessageBoxButton.OKCancel);
                if (msgRes == MessageBoxResult.OK)
                {
                    serviceQ.DeleteQuestion(dictQuestions[SelectedFrom]);
                    UpdateList();
                    MessageBox.Show("Питання видалено. Оновлюємо списки...");
                }
            }
            else
            {
                MessageBox.Show("Не обрано не одного питання");
            }
        }

        #endregion

        public void UpdateList()
        {
            QuestionListFrom.Clear();
            dictQuestions = dataQuestion.GetQuestionDictionary();
            foreach (var item in dictQuestions.Keys.ToList())
            {
                QuestionListFrom.Add(item);
            }
        }
    }
}
