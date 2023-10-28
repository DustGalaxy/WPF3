using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using WPF3.Model;

namespace WPF3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // register other needed services here

        }

        protected override Window CreateShell()
        {
            using (var context = new Context()) ;
            var w = Container.Resolve<MainWindow>();
            return w;
        }


    }
}
