
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using WPF3.Infrastructure;
using WPF3.Services;
using User = WPF3.Model.Entities.User;

namespace WPF3.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
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
            Password.Dispose();
            Email = String.Empty;
        }

        #endregion

        public IRegionManager _regionManager;
        public LoginViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }


        #region Props

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


        private SecureString _password = new SecureString();
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


        #endregion


        #region LogInCommand

        DelegateCommand<object> _logInCommand = null;

        public DelegateCommand<object> LogInCommand =>
            _logInCommand ??= new DelegateCommand<object>(LogInCommandExecute);

        void LogInCommandExecute(object parameter)
        {
            PasswordBox box = (PasswordBox)parameter;
            ServiceUser serviceUser = new ServiceUser();
            User user = new User();
            if (!serviceUser.IsUserValid(Email))
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
                ErrorLoginStatus = "Login error";
            }
        }

        #endregion


        #region NavToRegister

        DelegateCommand _navToRegister = null;

        public DelegateCommand NavToRegister =>
            _navToRegister ??= new DelegateCommand(NavigateToRegister);

        public void NavigateToRegister()
        {

            _regionManager.RequestNavigate(Regions.ContentRegion, "Register");
        }

        #endregion


    }
}
