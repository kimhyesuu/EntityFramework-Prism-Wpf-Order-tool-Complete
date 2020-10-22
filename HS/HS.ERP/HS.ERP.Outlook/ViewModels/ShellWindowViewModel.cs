using HS.ERP.Outlook.Core.Dependency;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS.ERP.Outlook.ViewModels
{
    public class ShellWindowViewModel : IWindowResources
    {
        public ShellWindowViewModel()
        {
            WindowCloseCommand = new DelegateCommand(OnClose);
            DragMoveCommand = new DelegateCommand(OnDrag);
        }

        public Action WindowClose { get; set; }
        public Action WindowDragMove { get; set; }

        public DelegateCommand WindowCloseCommand { get; private set; }
        public DelegateCommand DragMoveCommand { get; private set; }

        public bool WindowCanClose()
        {
            return true;
        }

        private void OnClose()
        {
            WindowClose?.Invoke();
        }

        private void OnDrag()
        {
            WindowDragMove?.Invoke();
        }
    }
}
