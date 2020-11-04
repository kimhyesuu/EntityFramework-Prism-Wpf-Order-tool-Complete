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


            _regionManager.RegisterViewWithRegion(RegionNames.RegisterAccountRegion, typeof(RegisterAccount));
            _regionManager.RegisterViewWithRegion(RegionNames.RegisteredAccountListRegion, typeof(RegisteredAccountList));


            _regionManager.RegisterViewWithRegion(RegionNames.RegisterProductRegion, typeof(RegisterProduct));
            _regionManager.RegisterViewWithRegion(RegionNames.RegisteredProductListRegion, typeof(RegisteredProductList));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
