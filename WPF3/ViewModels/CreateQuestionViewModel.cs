using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF3.ViewModels
{
    internal class CreateQuestionViewModel : BindableBase, INavigationAware
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

        }

        public IRegionManager _regionmanager { get; private set; }
        public IDialogService _dialogService { get; private set; }
        public CreateQuestionViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionmanager = regionManager;
            _dialogService = dialogService;
        }


        private DelegateCommand _createquestionthemecommand;
        public DelegateCommand CreateQuestionThemeCommand => _createquestionthemecommand ??= new DelegateCommand(CreateQuestionTheme);

        public void CreateQuestionTheme()
        {
            _dialogService.ShowDialog("CreateQustionThemeDialog", r =>
            {
                if (r.Result == ButtonResult.Cancel)
                    MessageBox.Show("Пройшла відміна");
                else if (r.Result == ButtonResult.OK)
                    MessageBox.Show("Все окей! Оновлюємо списки...");
                // UpdateList
            });
        }

        private DelegateCommand _fdialogcommand;
        public DelegateCommand FDialogCommand => _fdialogcommand ??= new DelegateCommand(FDialog);

        private string _imgpreview;
        public string ImagePreview
        {
            get => _imgpreview;
            set => SetProperty(ref _imgpreview, value);
        }



        public void FDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png";
            fileDialog.Multiselect = false;

            var res = fileDialog.ShowDialog();

            if (res == true)
            {
                string path = fileDialog.FileName;
                ImagePreview = path;
                //MessageBox.Show(Directory.GetCurrentDirectory());
                //File.Copy(path, $"F:\\CSharp\\WPFapp1\\WPF3\\WPF3\\Model\\Resourse\\Images\\{fileDialog.SafeFileName}");
            }
        }
    }
}
