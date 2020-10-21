using HS.ERP.Core;
using Modules.Menu.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Menu
{
    public class MenuModule : IModule
    {
        private IRegionManager _regionManager;

        public MenuModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
            _regionManager.RegisterViewWithRegion(RegionNames.MenuBarRegion, typeof(SideBar));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
