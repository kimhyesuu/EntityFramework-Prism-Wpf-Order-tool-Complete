using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HS.ERP.Business.Models;
using HS.ERP.Business.Models.Enums;
using HS.ERP.Business.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;


namespace HS.ERP.Outlook.Core.Dialogs.ViewModels
{
   public class AccountInfoViewModel : BindableBase, IDialogAware
   {
      private Account _accountInfo;
      private Account _selectedAccountInfo;
      private ObservableCollection<Account> _accountList;
 
      #region 프로퍼티
      private IServiceLogic<Account> ServiceLogic { get; }

      public ObservableCollection<Account> Accounts
      {
         get { return _accountList; }
         set { SetProperty(ref _accountList, value); }
      }

      public Account AccountInfo
      {
         get { return _accountInfo; }
         set { SetProperty(ref _accountInfo, value); }
      }

      public Account SelectedAccountInfo
      {
         get { return _selectedAccountInfo; }
         set { SetProperty(ref _selectedAccountInfo, value); }
      }
      #endregion

      public AccountInfoViewModel()
      {        
        // ServiceLogic = new AccountService();
         DataInitialize();
         CommandInitialize();
      }

      private void CommandInitialize()
      {
         CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
         SaveAccountInfoCommand = new DelegateCommand(AddOrUpdate);
         MoveAccountInfoCommand = new DelegateCommand(MoveAccount);
         DeleteAccountInfoCommand = new DelegateCommand<object>(o => DeleteAccount(o));
      }

      private void DataInitialize()
      {
         Accounts = new ObservableCollection<Account>();
         AccountInfoInit();
      }

      private void AccountInfoInit()
      {
         AccountInfo = null; 
         AccountInfo = new Account();
         AccountInfo.EntityState = EntityStateOption.None;
      }

      public DelegateCommand<object> DeleteAccountInfoCommand { get; private set; }
      public DelegateCommand SaveAccountInfoCommand { get; private set; }
      public DelegateCommand MoveAccountInfoCommand { get; private set; }
      public DelegateCommand<string> CloseDialogCommand { get; private set; }

      private void DeleteAccount(object selectedList)
      {
         if (selectedList is null) return;
         
         var selectedAccount = selectedList as Account;

         if(MessageBox.Show($"{selectedAccount.CompanyName}을 삭제하시겠습니까?" 
            ,"정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            Accounts.Remove(selectedAccount);
            MessageBox.Show($"{selectedAccount.CompanyName}이 삭제되었습니다.");
         }
      }

      #region 거래처 정보를 저장하는 로직 
      private void AddOrUpdate()
      {      
         var accountInfo = AccountInfo;

         if (!IsCompatibility(accountInfo))
            return;

         // 1. add할 때 id값이 같은지 확인해바야대
         if (IsAdd(accountInfo))
         {
            var rd = new Random();
            accountInfo.AccountId = long.Parse(DateTime.Now.ToString("yyyyMMdd") + rd.Next(1, 1000));

            AddAccountInfo(accountInfo);
         }
         else
         {
            UpdateAccountInfo(accountInfo);
         }
      }

      private void UpdateAccountInfo(Account accountInfo)
      {
         // 1. 값을 변경 후 다시 리스트에 넣는다.
         var found = Accounts.First(x => x.AccountId == accountInfo.AccountId);
         int index = Accounts.IndexOf(found);
         accountInfo.EntityState = EntityStateOption.Updated;
         Accounts[index] = accountInfo;
         
         accountInfo = null;
         AccountInfoInit();
      }

      private void AddAccountInfo(Account account)
      {
         Accounts.Add(new Account
         {
            AccountId = account.AccountId,
            CompanyName = account.CompanyName,
            CompanyEmail = account.CompanyEmail,
            Address = account.Address,
            ContactName = account.ContactName,
            Department = account.Department,
            Position = account.Position,
            TelePrefix = account.TelePrefix,
            TelePhoneNumber = account.TelePhoneNumber,
            FullPhoneNumber = account.TelePrefix + account.TelePhoneNumber,
            Description = account.Description,
            CreatedDate = account.CreatedDate,
            EntityState = EntityStateOption.Inserted
         });

         account = null;
         AccountInfoInit();
      }

      private bool IsCompatibility(Account accountInfo)
      {
         var accounts = Accounts;

         if (!(accountInfo.CompanyEmail != null
            && accountInfo.Address != null
            && accountInfo.ContactName != null
            ))
         {
            return false;
         } 
         else if (!IsNumeric(accountInfo.TelePhoneNumber))
         {
            MessageSend(accountInfo.TelePhoneNumber, "숫자로 입력해주세요.");
            return false;
         }

         if(accountInfo.EntityState is EntityStateOption.None)
         {
            var CompanyName = accounts.Where(x => x.CompanyName == accountInfo.CompanyName)
               .Select(same => same.CompanyName).FirstOrDefault();

            if (CompanyName == accountInfo.CompanyName)
               return false;
         }
         else if(accountInfo.EntityState is EntityStateOption.Inserted || accountInfo.EntityState is EntityStateOption.Updated)
         {
            var CompanyName = accounts.Where(x => x.CompanyName == accountInfo.CompanyName)
               .Select(same => same.CompanyName).Skip(1).FirstOrDefault();

            if (CompanyName != null)
               return false;
         }
         
         return true;
      }

      private bool IsAdd(Account accountInfo)
        => accountInfo.EntityState is EntityStateOption.None;

      private void MoveAccount()
         => AccountInfo = SelectedAccountInfo;

      private void MessageSend(object para, string message)      
        => MessageBox.Show($"{para}을 {message}");
 
      private bool IsNumeric(string telePhoneNumber)
        => int.TryParse(telePhoneNumber, out int Check);

      #endregion

      //collection값을 넣어주면 되겠네
      #region 다이얼로그 리절트를 받기 위해 쓰이는 기술
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
         //Message = parameters.GetValue<string>("message");
      }

      #endregion

      #region TitleName

      private string _message;
      public string Message
      {
         get => _message;
         set { SetProperty(ref _message, value); }
      }

      public string Title => "Account";

      #endregion
   }


}
