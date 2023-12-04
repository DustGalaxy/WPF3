using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF3.ViewModels
{
    class RegisterViewModel : BindableBase
    {
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
    }
}
