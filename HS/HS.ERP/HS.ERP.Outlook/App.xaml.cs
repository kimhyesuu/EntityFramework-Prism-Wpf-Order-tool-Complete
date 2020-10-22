using System.Windows;
using HS.ERP.Outlook.Views;
using Modules.Menu;
using Modules.Order;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;

namespace HS.ERP.Outlook
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<OrderModule>();
            moduleCatalog.AddModule<MenuModule>(); 
        }
    }
}
