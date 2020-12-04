using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HS.ERP.Business.Managers;
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
      private IRepogitoryManager<Account> RepogitoryManager { get; set; }

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

      public List<Account> DeletedAccounts { get; set; }

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
         SelectedAccountInfoCommand = new DelegateCommand<object>(DeleteAccount);
         AccountInfoDialogCommand = new DelegateCommand<string>(CloseDialog);
      }

      private void DataInitialize()
      {
         DeletedAccounts = new List<Account>();
         RepogitoryManager = new AccountManager();
         var result = RepogitoryManager.GetAll();
         if(result != null)
         {
            Accounts = new ObservableCollection<Account>(result);
         }
         else
         {
            Accounts = new ObservableCollection<Account>();
         }
         AccountInfoInit();
      }

      private void AccountInfoInit()
      {
         AccountInfo = null;
         AccountInfo = new Account(Newid());
         AccountInfo.EntityState = EntityStateOption.None;
      }

      public DelegateCommand<string> AccountInfoDialogCommand { get; private set; }
      public DelegateCommand<object> SelectedAccountInfoCommand { get; private set; }
      public DelegateCommand<string> MoveAccountInfoToListCommand { get; private set; }

      #region 거래처 정보를 관련 로직 

      private void DeleteAccount(object selectedList)
      {
         if (selectedList is null) return;
         
         var selectedAccount = selectedList as Account;

         if(MessageBox.Show($"{selectedAccount.CompanyName}을 삭제하시겠습니까?" 
            ,"정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            selectedAccount.EntityState = EntityStateOption.Deleted;
            DeletedAccounts.Add(selectedAccount);
            Accounts.Remove(selectedAccount);
         }
      }

      private void AddOrUpdate(string account)
      {
         var receivedInfo = ConvertStringToAccountInfo(account);

         if (!IsCompatibility(receivedInfo))
            return;

         if (IsAdd(receivedInfo.EntityState))
         {         
            AddAccountInfo(receivedInfo);
         }
         else
         {
            UpdateAccountInfo(receivedInfo);
         }
      }

      private Account ConvertStringToAccountInfo(string account)
      {
         string[] accountInfo = account.Split(':');

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
         };

         return receivedInfo;
      }

      private void UpdateAccountInfo(Account accountInfo)
      {
         accountInfo.EntityState = EntityStateOption.Updated;
         accountInfo.UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
         Accounts.Insert(Accounts.IndexOf(AccountInfo), accountInfo);
         Accounts.Remove(AccountInfo);

         accountInfo = null;
         AccountInfoInit();
      }

      private void AddAccountInfo(Account accountInfo)
      {     
         accountInfo.EntityState = EntityStateOption.Inserted;
         accountInfo.CreatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
         Accounts.Add(accountInfo);
                  
         accountInfo = null;
         AccountInfoInit();
      }

      private bool IsCompatibility(Account accountInfo)
      {
         if (string.IsNullOrEmpty(accountInfo.CompanyName) ||
             string.IsNullOrEmpty(accountInfo.ContactName) ||
             string.IsNullOrEmpty(accountInfo.TelePhoneNumber) ||
             string.IsNullOrEmpty(accountInfo.Address) ||
             string.IsNullOrEmpty(accountInfo.CompanyEmail))
         {
            return false;
         }

         if (!IsNumeric(accountInfo.TelePhoneNumber))
         {
            MessageSend(accountInfo.TelePhoneNumber, "숫자로 입력해주세요.");
             return false;
         }

         var AccountsToCompare = Accounts;

         foreach (var companyName in AccountsToCompare.Where(x => x.CompanyName == accountInfo.CompanyName)
             .Select(same => same.CompanyName))
         {
            if (!string.IsNullOrEmpty(companyName))
               return false;
         }

         foreach (var telePhoneNumber in AccountsToCompare.Where(x => x.TelePhoneNumber == accountInfo.TelePhoneNumber)
          .Select(same => same.TelePhoneNumber))
         {
            if (!string.IsNullOrEmpty(telePhoneNumber))
               return false;
         }

         return true;
      }

      private long? Newid()
      {
         var rd = new Random();
         return long.Parse(DateTime.Now.ToString("yyyyMMdd") + rd.Next(1, 1000));
      }

      private bool IsAdd(EntityStateOption accountInfo)
        => accountInfo is EntityStateOption.None;

      private void MessageSend(object para, string message)      
        => MessageBox.Show($"{para}을 {message}","NG");
 
      private bool IsNumeric(string telePhoneNumber)
        => int.TryParse(telePhoneNumber, out int Check);

      #endregion

      #region 저장된 값을 Dialog Result값에 포함시키기 위함

      public event Action<IDialogResult> RequestClose;
     
      private void CloseDialog(string CheckedResult)
      {       
         ButtonResult result = ButtonResult.None;
         var transportParameter = new DialogParameters();
         IEnumerable parameterValue = null;
         var savedResult = AccountListToSave(Accounts);        

         //확인 누를때만 db이동 시키고 아무거나 

         if ((CheckedResult?.ToLower() == "false" ||
             CheckedResult?.ToLower() == "true") &&
            DeletedAccounts.Count > 0 )
         {
            result = ButtonResult.OK;
         }

         if (CheckedResult?.ToLower() == "true" &&
            savedResult.FirstOrDefault() != null &&
            MessageBox.Show($"리스트를 저장하시겠습니까?", "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            result = ButtonResult.OK;
            parameterValue = savedResult;
         }
         else if (CheckedResult?.ToLower() == "false"
            && savedResult.FirstOrDefault() != null
            )
         {
            if(MessageBox.Show($"저장되지 않은 정보가 있습니다.\n저장하시겠습니까?", "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
               result = ButtonResult.OK;
               parameterValue = savedResult;
            }         
         }

         RaiseRequestClose(result, GetDialogParameters(transportParameter, parameterValue));
         Accounts = null;
         DeletedAccounts = null;
         AccountInfo = null;
      }

      private IEnumerable<Account> AccountListToSave(ObservableCollection<Account> accounts)     
         => accounts.Where(account => account.EntityState != EntityStateOption.DBUpdated);     

      private DialogParameters GetDialogParameters(DialogParameters transportParameter, IEnumerable parameterValue)
      {
         if(parameterValue != null)
         {
            foreach (var test in parameterValue)
            {
               transportParameter.Add("UpdateInformation", test);
            }   
         }

         if(DeletedAccounts.Count > 0)
         {
            foreach (var test in DeletedAccounts)
            {
               transportParameter.Add("UpdateInformation", test);
            }
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

      #region Dialog Window Resource

      public string Title => "거래처 정보";

      public int DialogWindowWith => 1100;

      #endregion
   }

}
