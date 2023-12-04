using Prism.Services.Dialogs;
using System.ComponentModel;
using System.Windows;

namespace WPF3.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window, IViewManager
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }

        public bool? Open(IDialogParameters dialogParameters)
        {
            this.DataContext = dialogParameters;
            return this.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
    }

    public interface IViewManager
    {
        bool? Open(IDialogParameters data);
    }
}
