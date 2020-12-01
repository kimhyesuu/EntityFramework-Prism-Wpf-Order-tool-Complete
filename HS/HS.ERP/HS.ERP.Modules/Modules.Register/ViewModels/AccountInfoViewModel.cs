using System;
using System.Collections;
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

      public bool CanSaveExcute
      {
         get => true;
         set => MoveAccountInfoToListCommand.RaiseCanExecuteChanged();
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
         MoveAccountInfoToListCommand = new DelegateCommand<string>(AddOrUpdate).ObservesCanExecute(() => CanSaveExcute);
         DeleteAccountInfoCommand = new DelegateCommand<object>(DeleteAccount);
         SaveAccountListCommand = new DelegateCommand<string>(SaveAccountListDialog);
      }

      private void DataInitialize()
      {
         Accounts = new ObservableCollection<Account>();
         AccountInfoInit();
      }

      private void AccountInfoInit()
      {
         AccountInfo = null;
         AccountInfo = new Account(Newid());
         AccountInfo.EntityState = EntityStateOption.None;
      }

      public DelegateCommand<string> SaveAccountListCommand { get; private set; }
      public DelegateCommand<object> DeleteAccountInfoCommand { get; private set; }
      public DelegateCommand<string> MoveAccountInfoToListCommand { get; private set; }

      #region 거래처 정보를 관련 로직 
      private void DeleteAccount(object selectedList)
      {
         if (selectedList is null) return;
         
         var selectedAccount = selectedList as Account;

         if(MessageBox.Show($"{selectedAccount.CompanyName}을 삭제하시겠습니까?" 
            ,"정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            Accounts.Remove(selectedAccount);
         }
      }

      private void AddOrUpdate(string account)
      {
         string[] accountInfo = account.Split(':');

         if (!IsCompatibility(accountInfo))
            return;

         var accounts = Accounts;

         var receivedInfo = new Account
         {
            AccountId = AccountInfo.AccountId,
            CompanyName = accountInfo[0],
            CompanyEmail = accountInfo[1],
            Address = accountInfo[2],
            ContactName = accountInfo[4],
            Department = accountInfo[5],
            Position = accountInfo[6],
            TelePrefix = accountInfo[7],
            TelePhoneNumber = accountInfo[8],
            FullPhoneNumber = accountInfo[7] + accountInfo[8],
            Description = accountInfo[3],
            EntityState = AccountInfo.EntityState,
            CreatedDate = null
         };

         if (IsAdd(receivedInfo))
         {
            AddAccountInfo(receivedInfo);
         }
         else
         {
            UpdateAccountInfo(receivedInfo);
         }
      }
      private void UpdateAccountInfo(Account accountInfo)
      {
         accountInfo.EntityState = EntityStateOption.Updated;
         Accounts.Insert(Accounts.IndexOf(AccountInfo), accountInfo);
         Accounts.Remove(AccountInfo);

         accountInfo = null;
         AccountInfoInit();
      }

      private void AddAccountInfo(Account accountInfo)
      {
         Accounts.Add(new Account
         {
            AccountId = accountInfo.AccountId,
            CompanyName = accountInfo.CompanyName,
            CompanyEmail = accountInfo.CompanyEmail,
            Address = accountInfo.Address,
            ContactName = accountInfo.ContactName,
            Department = accountInfo.Department,
            Position = accountInfo.Position,
            TelePrefix = accountInfo.TelePrefix,
            TelePhoneNumber = accountInfo.TelePhoneNumber,
            FullPhoneNumber = accountInfo.TelePrefix + accountInfo.TelePhoneNumber,
            Description = accountInfo.Description,
            CreatedDate = accountInfo.CreatedDate,
            EntityState = EntityStateOption.Inserted
         });

         accountInfo = null;
         AccountInfoInit();
      }

      private bool IsCompatibility(string[] accountInfo)
      {
         //시간 남으면 수정
         var accounts = Accounts;

         foreach (var info in accountInfo)
         {
            if (string.IsNullOrEmpty(info) && accountInfo[3] != info)
            {
               return false;
            }
         }

         if(!IsNumeric(accountInfo[8]))
         {
            MessageSend(accountInfo[8], "숫자로 입력해주세요.");
             return false;
         }

         // 킵 이것을 manager에게 보내면 되겠다.
         var companyNames = accounts.Where(x => x.CompanyName == accountInfo[0])
               .Select(same => same.CompanyName);

         foreach (var companyName in companyNames)
         {
            if (!string.IsNullOrWhiteSpace(companyName))
               return false;
         }

         return true;
      }

      private long? Newid()
      {
         var rd = new Random();
         return long.Parse(DateTime.Now.ToString("yyyyMMdd") + rd.Next(1, 1000));
      }

      private bool IsAdd(Account accountInfo)
        => accountInfo.EntityState is EntityStateOption.None;

      private void MessageSend(object para, string message)      
        => MessageBox.Show($"{para}을 {message}","NG");
 
      private bool IsNumeric(string telePhoneNumber)
        => int.TryParse(telePhoneNumber, out int Check);

      #endregion

      #region 저장된 값을 Dialog Result값에 포함시키기 위함

      public event Action<IDialogResult> RequestClose;

      private void SaveAccountListDialog(string CheckedResult)
      {
         // CanSave true false
         // 여기서 거래처 정보를 받아서 db로 넘길 것을 확인 조건부까지
         ButtonResult result = ButtonResult.None;
         var transportParameter = new DialogParameters();
         var CheckUpdatedaccounts = Accounts;
         IEnumerable parameterValue = null;

         var savedResult = CheckUpdatedaccounts.Where(account => account.EntityState != EntityStateOption.None);

         if (CheckedResult?.ToLower() == "true" && savedResult.FirstOrDefault() is null)
         {
            MessageBox.Show("변경한 거래 목록이 없습니다.", "NG", MessageBoxButton.OK);
         }
         else if (CheckedResult?.ToLower() == "true" && MessageBox.Show($"리스트를 저장하시겠습니까?"
            , "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            result = ButtonResult.OK;
            parameterValue = savedResult;
         }

         RaiseRequestClose(result, GetDialogParameters(transportParameter, parameterValue));
      }

      private DialogParameters GetDialogParameters(DialogParameters transportParameter, IEnumerable parameterValue)
      {
         foreach (var test in parameterValue)
         {
            transportParameter.Add("UpdateInformation", test);
         }

         return transportParameter;
      }

      private void RaiseRequestClose(ButtonResult dialogResult, DialogParameters dialogParameters)
        => RequestClose?.Invoke(new DialogResult(dialogResult, dialogParameters));

      public bool CanCloseDialog()
         => true;

      public void OnDialogClosed() { }
     
      public void OnDialogOpened(IDialogParameters parameters) { }

      #endregion

      #region TitleName

      public string Title => "거래처 정보";

      public int DialogWindowWith => 1100;

      #endregion
   }

}
