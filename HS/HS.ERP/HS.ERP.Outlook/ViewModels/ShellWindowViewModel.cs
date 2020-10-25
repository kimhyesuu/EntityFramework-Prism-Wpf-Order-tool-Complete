using HS.ERP.Outlook.Core.Dependency;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace HS.ERP.Outlook.ViewModels
{
    public class ShellWindowViewModel : BindableBase, IWindowResources
    {
        private readonly IDialogService _dialogService;

        public Action WindowClose { get; set; }
        public Action WindowDragMove { get; set; }

        public ShellWindowViewModel(IDialogService dialogService)
        {
            this._dialogService = dialogService;
            WindowCloseCommand = new DelegateCommand(OnClose);
            DragMoveCommand = new DelegateCommand(OnDrag);
            RegisterShowDialogCommand = new DelegateCommand(ShowDialog);
        }

        public DelegateCommand WindowCloseCommand { get; private set; }
        public DelegateCommand DragMoveCommand { get; private set; }
        public DelegateCommand RegisterShowDialogCommand { get; private set; }

        public bool WindowCanClose() => true;
        private void OnClose() => WindowClose?.Invoke();
        private void OnDrag() => WindowDragMove?.Invoke();
        private void ShowDialog()
        {
            
        }

        private bool IsNullOrParameter(IDialogParameters parameters)
        {
            return parameters.GetValue<string>("submessage") != string.Empty;
        }


        #region TitleName Property

      
        public string ButtonTitle { get => "Show Dialog"; }

        #endregion

      
    }
}
