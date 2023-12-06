
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WPF3.Infrastructure;
using WPF3.Model;
using WPF3.Model.Entities;
using WPF3.Services;
using WPF3.Views;
using User = WPF3.Model.Entities.User;

namespace WPF3.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        public IRegionManager _regionManager;
        public LoginViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }


        private string _errorLoginStatus;
        public string ErrorLoginStatus
        {
            get => _errorLoginStatus;
            set => SetProperty<string>(ref _errorLoginStatus, value);
        }

        private string _errorPasswordStatus;
        public string ErrorPasswordStatus
        {
            get => _errorLoginStatus;
            set => SetProperty<string>(ref _errorPasswordStatus, value);
        }


        private SecureString _password;
        public SecureString Password
        {
            get => _password;
            set => SetProperty<SecureString>(ref _password, value);
        }



        private string _email = "";
        public string Email
        {
            get => _email;
            set => SetProperty<string>(ref _email, value);
        }


        private string _debug = "";
        public string Debug1
        {
            get => _debug;
            set => SetProperty<string>(ref _debug, value);
        }

        private readonly ServiceUser serviceUser = new ServiceUser();
        private IDialogParameters dialogParameters;

        DelegateCommand<object> _logInCommand = null;

        public DelegateCommand<object> LogInCommand =>
            _logInCommand ??= new DelegateCommand<object>(LogInCommandExecute);




        void LogInCommandExecute(object parameter)
        {
            PasswordBox box = (PasswordBox)parameter;
            ServiceUser serviceUser = new ServiceUser();
            User user = new User();
            if (!serviceUser.ValidateUser(Email))
            {
                
                try
                {
                    Debug1 = Email + " " + box.SecurePassword;
                    user = serviceUser.LoginUser(Email, box.SecurePassword);
                    
                    switch (user.UserType)
                    {
                        case 1:
                            var param = new NavigationParameters
                            {
                                { "user", user }
                            };
                            _regionManager.RequestNavigate(Regions.ContentRegion, "Admin", param);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ErrorPasswordStatus = "Password error";
                }
                
            }
            else
            {
                // _regionManager.RequestNavigate(Regions.ContentRegion, "Admin");
                ErrorLoginStatus = "Login error";
            }
        }

        DelegateCommand _navToRegister = null;

        public DelegateCommand NavToRegister =>
            _navToRegister ??= new DelegateCommand(NavigateToRegister);

        public void NavigateToRegister()
        {

            _regionManager.RequestNavigate(Regions.ContentRegion, "Register", new NavigationParameters());
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ServiceUser userServices = new ServiceUser();
            User user = new()
            {
                UserType = 1,
                Surname = "Test",
                Name = "admin",
                Password = "admin",
                Email = "admin"
            };
            userServices.CreateUser(user);
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
