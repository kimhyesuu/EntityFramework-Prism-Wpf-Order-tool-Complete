using HS.ERP.Outlook.Core.Dependency;
using Prism.Commands;
using System;

namespace HS.ERP.Outlook.ViewModels
{
    public class ShellWindowViewModel : IWindowResources
    {
        public Action WindowClose { get; set; }
        public Action WindowDragMove { get; set; }

        public ShellWindowViewModel()
        {
            WindowCloseCommand = new DelegateCommand(OnClose);
            DragMoveCommand = new DelegateCommand(OnDrag);
        }

        public DelegateCommand WindowCloseCommand { get; private set; }
        public DelegateCommand DragMoveCommand { get; private set; }

        public bool WindowCanClose() => true;
        private void OnClose() => WindowClose?.Invoke();
        private void OnDrag() => WindowDragMove?.Invoke();
    }
}
