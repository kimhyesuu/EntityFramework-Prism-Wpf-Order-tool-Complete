using Prism.Commands;
using Prism.Mvvm;
using System;
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
      
        public SideBarViewModel()
        {
            IsOpenMenu = true;
            IsCloseMenu = false;

            ReverseCommand = new DelegateCommand<Object>(o => ReverseButton(o));
        }

        public DelegateCommand<Object> ReverseCommand { get; private set; }

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
    }
}
