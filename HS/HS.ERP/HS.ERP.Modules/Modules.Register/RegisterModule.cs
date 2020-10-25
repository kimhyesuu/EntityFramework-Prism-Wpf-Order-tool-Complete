using HS.ERP.Core;
using Modules.Register.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Modules.Register
{
    public class RegisterModule : IModule
    {
        private IRegionManager _regionManager;


        public RegisterModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;


            _regionManager.RegisterViewWithRegion(RegionNames.RegisterRegion, typeof(RegisterAccount));
            _regionManager.RegisterViewWithRegion(RegionNames.RegisteredListRegion, typeof(RegisteredAccountList));


            _regionManager.RegisterViewWithRegion(RegionNames.RegisterRegion, typeof(RegisterProduct));
            _regionManager.RegisterViewWithRegion(RegionNames.RegisteredListRegion, typeof(RegisteredProductList));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
