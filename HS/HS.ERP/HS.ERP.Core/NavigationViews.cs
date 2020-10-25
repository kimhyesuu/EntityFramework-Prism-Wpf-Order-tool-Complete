using Prism.Regions;

namespace HS.ERP.Core
{
    public static class NavigationViews
    {
        public static void Location(IRegionManager regionManager, string regionName, string viewName)
        {
            regionManager.RequestNavigate(regionName, viewName);
        }
    }
}
