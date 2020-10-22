using HS.ERP.Core;
using Modules.Menu.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Modules.Menu
{
    public class MenuModule : IModule
    {
        private IRegionManager _regionManager;

        public MenuModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
           
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MenuBarRegion, typeof(SideBar));           
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
