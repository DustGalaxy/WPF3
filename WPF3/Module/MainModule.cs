using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using WPF3.Infrastructure;
using WPF3.Views;

namespace WPF3.Module
{
    internal class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();
            region.RegisterViewWithRegion(Regions.ContentRegion, typeof(Login));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Admin>("Admin");
            containerRegistry.RegisterForNavigation<Manager>("Manager");
            containerRegistry.RegisterForNavigation<User>("User");
            containerRegistry.RegisterForNavigation<Register>("Register");
            containerRegistry.RegisterForNavigation<Login>("Login");
            containerRegistry.RegisterForNavigation<CreateTest>("CreateTest");
            containerRegistry.RegisterForNavigation<CreateQuestion>("CreateQuestion");
            containerRegistry.RegisterForNavigation<CreateQuestionTheme>("CreateQuestionTheme");
            containerRegistry.RegisterForNavigation<Test>("Test");
        }
    }
}
