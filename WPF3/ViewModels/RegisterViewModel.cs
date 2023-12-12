using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Regions;
using WPF3.Infrastructure;
using WPF3.Services;
using WPF3.Views;
using User = WPF3.Model.Entities.User;

namespace WPF3.ViewModels
{
    class RegisterViewModel : BindableBase, INavigationAware
    {

        #region Navigate

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ErrorLoginStatus = String.Empty;
            ErrorPasswordStatus = String.Empty;
            Email = String.Empty;
            Name = String.Empty;
            Surname = String.Empty;
            Passw = String.Empty;
        }

        #endregion


        private IRegionManager _regionManager;
        public RegisterViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region NavToLoginCommand

        private DelegateCommand _navToLoginCommand;
        public DelegateCommand NavToLoginCommand => _navToLoginCommand ??= new DelegateCommand(NavToLogin);

        private void NavToLogin()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "Login");
        }

        #endregion


        #region Props

        private string _errorLoginStatus = "";
        public string ErrorLoginStatus
        {
            get => _errorLoginStatus;
            set => SetProperty<string>(ref _errorLoginStatus, value);
        }

        private string _errorPasswordStatus = "";
        public string ErrorPasswordStatus
        {
            get => _errorLoginStatus;
            set => SetProperty<string>(ref _errorPasswordStatus, value);
        }

        private string _email = "";
        public string Email
        {
            get => _email;
            set => SetProperty<string>(ref _email, value);
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set => SetProperty<string>(ref _name, value);
        }

        private string _surname = "";
        public string Surname
        {
            get => _surname;
            set => SetProperty<string>(ref _surname, value);
        }
        private string _passw = "";
        public string Passw
        {
            get => _passw;
            set => SetProperty<string>(ref _passw, value);
        }
        
        #endregion


        #region RegisterCommand

        private DelegateCommand<PasswordBox> _registerCommand;

        public DelegateCommand<PasswordBox> RegisterCommand => _registerCommand ??= new DelegateCommand<PasswordBox>(Register);

        private void Register(PasswordBox obj)
        {
            
            SecureString _passOne = obj.SecurePassword;
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Заповніть усі поля!", "Помилка");
                return;
            }

            if (_passOne.Length < 4)
            {
                MessageBox.Show(_passOne.Length.ToString());
                MessageBox.Show("Пароль повинен бути не меньш 4 символів!", "Помилка");
                return;
            }

            Services.PassHasher passHasher = new PassHasher();
            Services.ServiceUser serviceUser = new ServiceUser();
            if (serviceUser.IsUserValid(Email))
            {
                User user = new User()
                {
                    Email = Email,
                    Name = Name,
                    Password = passHasher.GetSecureHash(_passOne),
                    Surname = Surname,
                    UserType = 3
                };

                if (serviceUser.CreateUser(user))
                {

                    MessageBox.Show(
                        $"Ви зареєструвалися,{Name} {Surname}!\nЩоб почати роботу увійдіть в аккаунт",
                        "Вдала реєстрація користувача");
                }
                else
                {
                    MessageBox.Show(
                        $"Щось пішло не так... \nЗверніться до адміністрації", 
                        "Помилка");
                }
            }
            else
            {
                ErrorLoginStatus = "Така почта вже зареєстрована!";
            }
        }

        #endregion


    }
}
