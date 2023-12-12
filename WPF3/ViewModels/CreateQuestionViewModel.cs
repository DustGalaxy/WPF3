using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using WPF3.Infrastructure;
using WPF3.Model.Entities;
using WPF3.Services;

namespace WPF3.ViewModels
{
    internal class CreateQuestionViewModel : BindableBase, INavigationAware
    {
        #region INavigate
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            QThemeName = serviceQuestionTheme.GetThemeDictionary();
            UpadateThemeList();
        }
        #endregion

        public IRegionManager _regionmanager { get; private set; }
        public IDialogService _dialogService { get; private set; }

        ServiceQuestionTheme serviceQuestionTheme = new ServiceQuestionTheme();
        Dictionary<string, QuestionTheme> QThemeName;

        public CreateQuestionViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionmanager = regionManager;
            _dialogService = dialogService;
        }

        #region Propertis
        public ObservableCollection<string> QuestionThemeList { get; private set; } = new ObservableCollection<string>();

        private string _ans = null;
        public string Ans
        {
            get => _ans;
            set => SetProperty<string>(ref _ans, value);
        }
        

        private string _wans1 = null;
        public string WAns1
        {
            get => _wans1;
            set => SetProperty<string>(ref _wans1, value);
        }

        private string _wans2 = null;
        public string WAns2
        {
            get => _wans2;
            set => SetProperty<string>(ref _wans2, value);
        }

        private string _wans3 = null;
        public string WAns3
        {
            get => _wans3;
            set => SetProperty<string>(ref _wans3, value);
        }

        private string _question = null;
        public string Question
        {
            get => _question;
            set => SetProperty<string>(ref _question, value);
        }

        private string _selectedTheme = null;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set => SetProperty<string>(ref _selectedTheme, value);
        }

        private string _imgName = null;
        public string ImageName
        {
            get => _imgName;
            set => SetProperty<string>(ref _imgName, value);
        }

        private string _imgpreView = null;
        public string ImagePreview
        {
            get => _imgpreView;
            set => SetProperty(ref _imgpreView, value);
        }


        private string _questionName = null;
        public string QuestionName
        {
            get => _questionName;
            set => SetProperty(ref _questionName, value);
        }


        #endregion

        #region CreateQuestionThemeCommand
        private DelegateCommand _createQuestionThemeCommand;
        public DelegateCommand CreateQuestionThemeCommand => _createQuestionThemeCommand ??= new DelegateCommand(CreateQuestionTheme);

        public void CreateQuestionTheme()
        {
            _dialogService.ShowDialog("CreateQustionThemeDialog", r =>
            {
                if (r.Result == ButtonResult.Cancel)
                    MessageBox.Show("Пройшла відміна");
                else if (r.Result == ButtonResult.OK)
                {
                    MessageBox.Show("Все окей! Оновлюємо списки...");
                }
            });
            UpadateThemeList();
        }
        #endregion

        #region CreateQuestionCommand
        private DelegateCommand _createQuestionCommand;
        public DelegateCommand CreateQuestionCommand => _createQuestionCommand ??= new DelegateCommand(CreateQuestion);

        public void CreateQuestion()
        {
            if (Question is null || 
                Ans is null || 
                WAns1 is null || 
                WAns2 is null || 
                WAns3 is null || 
                ImagePreview is null || 
                SelectedTheme is null || 
                QuestionName is null)
                MessageBox.Show("Заповніть усі поля!");
            else
            {
                Questions question = new Questions
                {
                    Question = Question,
                    Answer = Ans,
                    WAns1 = WAns1,
                    WAns2 = WAns2,
                    WAns3 = WAns3,
                    ImageSrc = SaveImage(ImagePreview, ImageName),
                    ThemeId = QThemeName[SelectedTheme].Id,
                    Name = QuestionName
                };
                Services.ServiceQuestion serviceQuestion = new Services.ServiceQuestion();
                serviceQuestion.CreateQuestion(question);
            }


        }
        #endregion

        #region NavToCreateTestCommand
        private DelegateCommand _navToCreateTestCommand;
        public DelegateCommand NavToCreateTestCommand => _navToCreateTestCommand ??= new DelegateCommand(NavToCreateTest);

        public void NavToCreateTest()
        {
            _regionmanager.RequestNavigate(Regions.ContentRegion, "CreateTest");
        }
        #endregion

        #region Image dialog and save
        private DelegateCommand _fdialogcommand;
        public DelegateCommand FDialogCommand => _fdialogcommand ??= new DelegateCommand(FDialog);

        private string SaveImage(string path, string filename)
        {
            string final_path = $"F:\\CSharp\\WPFapp1\\WPF3\\WPF3\\Model\\Resourse\\Images\\{filename}";
            try
            {
                File.Copy(path, final_path);
            }
            catch (Exception)
            {

            }
            
            return final_path;
        }

        public void FDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png";
            fileDialog.Multiselect = false;

            var res = fileDialog.ShowDialog();

            if (res == true)
            {
                ImagePreview = fileDialog.FileName;
                ImageName = fileDialog.SafeFileName;
            }
        }
        #endregion

        #region UpadateThemeList
        private void UpadateThemeList()
        {
            QuestionThemeList.Clear();
            QThemeName = serviceQuestionTheme.GetThemeDictionary();
            foreach (var item in QThemeName.Keys.ToList())
                QuestionThemeList.Add(item);
        }
        #endregion
    }
}
