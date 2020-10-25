using HS.ERP.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;

namespace Modules.Menu.ViewModels
{
    internal static class MenuBarCommandParameter
    {
        internal const string btnOpenMenu = "btnOpenMenu";
        internal const string btnCloseMenu = "btnCloseMenu";
    }

    public class SideBarViewModel : BindableBase
    {
        private bool _isOpenMenu;
        private bool _isCloseMenu;
        private IRegionManager _regionManager;

        public bool IsOpenMenu
        {
            get { return _isOpenMenu; }
            set { SetProperty(ref _isOpenMenu, value); }
        }

        public bool IsCloseMenu
        {
            get { return _isCloseMenu; }
            set { SetProperty(ref _isCloseMenu, value); }
        }
      
        public SideBarViewModel(IRegionManager regionManager)
        {
            IsOpenMenu = true;
            IsCloseMenu = false;
            this._regionManager = regionManager;
            ReverseCommand = new DelegateCommand<object>(o => ReverseButton(o));
            ViewsNavigationCommand = new DelegateCommand<object>(o => OnNavigation(o));
        }

        public DelegateCommand<object> ReverseCommand { get; private set; }
        public DelegateCommand<object> ViewsNavigationCommand { get; private set; }

        //When Button Clicked, Button Element(Visibility) invert  
        private void ReverseButton(object parameter)
        {
            if (parameter is null) return;

            var para = parameter as FrameworkElement;

            if (para.Name == MenuBarCommandParameter.btnOpenMenu || para.Name == MenuBarCommandParameter.btnCloseMenu)
            {
                IsCloseMenu = !IsCloseMenu;
                IsOpenMenu ^= true;              
            }
        }

        private void OnNavigation(object navigationPath)
        {
            if (navigationPath is null) return;

            var para = navigationPath as FrameworkElement;            
            NavigationViews.Location(_regionManager, RegionNames.ContentRegion, para.Name);           
        }
    }
}
