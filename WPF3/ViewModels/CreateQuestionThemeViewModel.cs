using ControlzEx.Standard;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF3.Model.Entities;
using WPF3.Services;

namespace WPF3.ViewModels
{
    internal class CreateQuestionThemeViewModel : BindableBase, IDialogAware
    {
        private string _title = "Створити нову тему для питання";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public event Action<IDialogResult> RequestClose;

        private string _themename;
        public string ThemeName
        {
            get => _themename;
            set => SetProperty(ref _themename, value);
        }

        private string _themediscription;
        public string ThemeDiscription
        {
            get => _themediscription;
            set => SetProperty(ref _themediscription, value);
        }

        private DelegateCommand _closeDialogCommand;
        public DelegateCommand CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand(CloseDialog));

        private DelegateCommand _addThemeCommand;
        public DelegateCommand AddThemeCommand =>
            _addThemeCommand ?? (_addThemeCommand = new DelegateCommand(AddTheme));


        private void AddTheme()
        {

            if (string.IsNullOrEmpty(ThemeDiscription) || string.IsNullOrEmpty(ThemeName))
            {
                MessageBox.Show("У теми немає назви або опису!", "Помилка");
                return;
            }

            ServiceQuestionTheme serviceQuestionTheme = new ServiceQuestionTheme();
            serviceQuestionTheme.CreateQustionTheme(new QuestionTheme
            {
                Name = ThemeName,
                Description = ThemeDiscription
            });
            
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }


        protected virtual void CloseDialog()
        { 
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
