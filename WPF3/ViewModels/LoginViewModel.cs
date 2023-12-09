
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

            User admin = new User
            {
                Name = "admin",
                Email = "admin",
                Surname = "admin",
                Password = "admin",
                UserType = 1,
            };

            User manager = new User
            {
                Name = "manager",
                Email = "manager",
                Surname = "manager",
                Password = "manager",
                UserType = 2,
            };

            User user = new User
            {
                Name = "user",
                Email = "user",
                Surname = "user",
                Password = "user",
                UserType = 3,
            };

            serviceUser.CreateUser(admin);
            serviceUser.CreateUser(manager);
            serviceUser.CreateUser(user);
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
            //_regionManager.RequestNavigate(Regions.ContentRegion, "Admin", new NavigationParameters{{"userId", 2}});
            //return;
            PasswordBox box = (PasswordBox)parameter;
            ServiceUser serviceUser = new ServiceUser();
            User user = new User();
            if (!serviceUser.ValidateUser(Email))
            {
                
                try
                {
                    Debug1 = Email + " " + box.SecurePassword;
                    user = serviceUser.LoginUser(Email, box.SecurePassword);
                    var param = new NavigationParameters
                    {
                        { "user", user }
                    };
                    switch (user.UserType)
                    {
                        case 1:

                            _regionManager.RequestNavigate(Regions.ContentRegion, "Admin", param);
                            break;

                        case 2:
                            _regionManager.RequestNavigate(Regions.ContentRegion, "Manager", param); 
                            break;

                        case 3:
                            _regionManager.RequestNavigate(Regions.ContentRegion, "User", param);
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
