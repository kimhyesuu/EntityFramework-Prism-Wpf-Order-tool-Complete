using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace HS.ERP.Outlook.Core.Dialogs.ViewModels
{
    public class AccountInfoViewModel : BindableBase, IDialogAware 
    {
        public AccountInfoViewModel()
        {
            CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
        }

        public DelegateCommand<string> CloseDialogCommand { get; private set; }

        public event Action<IDialogResult> RequestClose;

        private void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            var transportParameter = new DialogParameters();
            string parameterValue = string.Empty;

            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.OK;
                parameterValue = "ButtonResult.OK";
            }

            transportParameter.Add("submessage", parameterValue);
            RaiseRequestClose(result, transportParameter);
        }

        private void RaiseRequestClose(ButtonResult dialogResult, DialogParameters dialogParameters)
          => RequestClose?.Invoke(new DialogResult(dialogResult, dialogParameters));

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }

        #region TitleName

        private string _message;
        public string Message
        {
            get => _message;
            set { SetProperty(ref _message, value); }
        }
        public string ButtonOKTitle { get => "OK"; }
        public string ButtonCancelTitle { get => "Cancel"; }
        public string Title => "RegisterAccountDialog";

        #endregion
    }
}
