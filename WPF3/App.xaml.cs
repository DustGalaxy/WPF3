using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using WPF3.Model;
using WPF3.Module;
using WPF3.Views;
using User = WPF3.Model.Entities.User;

namespace WPF3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // register other needed services here
            containerRegistry.Register<Services.IDataResult, Services.ServicesResult>();
            containerRegistry.Register<Services.IDataQuestion, Services.ServiceQuestion>();
            containerRegistry.Register<Services.IDataMail, Services.ServiceMail>();
            containerRegistry.Register<Services.IDataTest, Services.ServiceTest>();
            containerRegistry.Register<Services.IDataTimeOut, Services.ServiceTimeOut>();
            containerRegistry.Register<Services.IDataQuestionTheme, Services.ServiceQuestionTheme>();
            containerRegistry.Register<Services.IDataUser, Services.ServiceUser>();
           

        }

        protected override Window CreateShell()
        {

            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<MainModule>();
        }

    }
}
