using HS.ERP.Core;
using Modules.Order.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Modules.Order
{
   public class OrderModule : IModule
   {
      private IRegionManager _regionManager;

      public OrderModule(IRegionManager regionManager)
      {
         this._regionManager = regionManager;
         _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(RegisterOrder));
      }

      public void OnInitialized(IContainerProvider containerProvider)
      {

      }

      public void RegisterTypes(IContainerRegistry containerRegistry)
      {
         containerRegistry.RegisterForNavigation<OrderedList>();
      }
   }
}
