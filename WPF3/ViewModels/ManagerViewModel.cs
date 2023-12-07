using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Shell;
using MahApps.Metro.Converters;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using WPF3.Infrastructure;
using WPF3.Model.Entities;
using WPF3.Services;

namespace WPF3.ViewModels
{
    public class ManagerViewModel : BindableBase, INavigationAware
    {

        #region Navigate

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            dictUsers = serviceUser.GetUsersDictionary();
            UserListLoad();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion


        private Dictionary<string, User> dictUsers;
        private Services.ServiceUser serviceUser = new();
        private Services.ServicesResult serviceResult = new();
        public IRegionManager _regionManager { get; private set; }
        public ManagerViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region Props

        public ObservableCollection<string> UserList { get; private set; } = new();
        public ObservableCollection<string> ResultList { get; private set; } = new();

        private string _textUserFilter;

        public string TextUserFilter
        {
            get => _textUserFilter;
            set => SetProperty(ref _textUserFilter, value);
        }

        private string _selectedUser;

        public string SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (SetProperty(ref _selectedUser, value))
                {
                    ResultList.Clear();
                    foreach (var item in serviceResult.GetUserResults(dictUsers[SelectedUser].UserId))
                    {
                        ResultList.Add($"Назва тесту: \"{item.Tests.Name}\" Результат: {item.Result}");
                    }
                }
            }
        }

        #endregion


        #region NavToLoginCommand

        private DelegateCommand _navToLoginCommand;

        public DelegateCommand NavToLoginCommand =>
            _navToLoginCommand ??= new DelegateCommand(NavToLogin);

        private void NavToLogin()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, "Login");
        }

        #endregion


        #region UpdateListByFilterCommand

        private DelegateCommand _updateListByFilterCommand;

        public DelegateCommand UpdateListByFilterCommand =>
            _updateListByFilterCommand ??= new DelegateCommand(UpdateListByFilter);

        private void UpdateListByFilter()
        {
            UserListLoad();
            ObservableCollection<string> tempList = new();

            tempList.AddRange(UserList);


            foreach (var item in UserList)
            {
                
                if (string.IsNullOrEmpty(TextUserFilter)) continue;
                if (TextUserFilter.ToLower().Contains(item.ToLower()) || item.ToLower().Contains(TextUserFilter.ToLower())) continue;
                tempList.Remove(item);
            }

            UserList.Clear();
            UserList.AddRange(tempList);
        }

        #endregion


        private void UserListLoad()
        {
            UserList.Clear();


            foreach (var user in dictUsers.Keys.ToList())
            {
                UserList.Add(user);
            }
        }
    }
}
