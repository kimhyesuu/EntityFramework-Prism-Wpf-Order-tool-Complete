using System.Windows;
using HS.ERP.Core;
using HS.ERP.Outlook.Core.Dialogs;
using HS.ERP.Outlook.Core.Dialogs.ViewModels;
using HS.ERP.Outlook.Core.Dialogs.Views;
using HS.ERP.Outlook.Views;
using Modules.Menu;
using Modules.Order;
using Modules.Order.Views;
using Modules.Register;
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
            //containerRegistry.RegisterSingleton<IDataManager<string>>();
            //containerRegistry.RegisterSingleton<IDataManager<string>>();
            containerRegistry.RegisterSingleton<IDataManager<Person>, DataManager<Person>>();
            containerRegistry.RegisterSingleton<IDataManager<PersonTwo>, DataManager<PersonTwo>>();

            containerRegistry.RegisterDialog<AccountInfo, AccountInfoViewModel>();
            containerRegistry.RegisterDialog<ProductInfo, ProductInfoViewModel>();
            
            containerRegistry.RegisterDialogWindow<RegistryDialogWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<OrderModule>();
            moduleCatalog.AddModule<MenuModule>();
            moduleCatalog.AddModule<RegisterModule>();
        }
    }
}
