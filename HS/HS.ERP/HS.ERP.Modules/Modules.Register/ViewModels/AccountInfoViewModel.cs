using System;
using System.Collections;
using System.Collections.Generic;
using HS.ERP.Business.Converter;
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
      private IDataService<Account> DataService { get; set; }

      public ObservableCollection<Account> Accounts
      {
         get { return _accountList; }
         set { SetProperty(ref _accountList, value); }
      }

      public Account SelectedAccount
      {
         get { return _accountInfo; }
         set { SetProperty(ref _accountInfo, value); }      
      }

      public List<Account> InnerAccounts { get; set; }

      public bool CanSaveUpdatedState { get; set; }

      public bool CanSaveExcute
      {
         get => true;
         set => MoveAccountInfoToListCommand.RaiseCanExecuteChanged();
      }

      public string AccountTitleHeader => "삭제";
      #endregion

      public AccountInfoViewModel()
      {        
         DataInitialize();
         CommandInitialize();
      }
      
      public DelegateCommand<string> AccountInfoDialogCommand { get; private set; }
      public DelegateCommand<object> SelectedAccountInfoCommand { get; private set; }
      public DelegateCommand<string> MoveAccountInfoToListCommand { get; private set; }
      public DelegateCommand RevertUpdateInfoCommand { get; private set; }


      #region 거래처 정보를 관련 로직 

      private void DeleteAccount(object selectedList)
      {        
         //여기서 오더의 유효성 판단 
         if (selectedList is null) return;
         
         var selectedAccount = selectedList as Account;

         if(MessageBox.Show($"{selectedAccount.CompanyName}을 삭제하시겠습니까?" 
            ,"정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            selectedAccount.EntityState = EntityStateOption.Deleted;
            InnerAccounts.Add(selectedAccount);
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
            if (IsStoredInfoFromDB(receivedInfo) is false)
            {
               CanSaveUpdatedState = false;
            }

            UpdateAccountInfo(receivedInfo);
         }
      }

      // 집가서 여기 확인해볼 것
      private bool IsStoredInfoFromDB(Account receivedInfo)
      {
         var result = RepogitoryManager.GetAll();

         if (result is null)
         {
            return false;
         }

         foreach (var info in result)
         {
            if (receivedInfo.AccountId == info.AccountId)
            {
               return true;
            }
         }

         return false;
      }

      private Account ConvertStringToAccountInfo(string account)
      {
         string[] accountInfo = account.Split(':');

         var receivedInfo = new Account
         {
            AccountId = SelectedAccount.AccountId,
            CompanyName = accountInfo[0],
            CompanyEmail = accountInfo[1],
            Address = accountInfo[2],
            ContactName = accountInfo[4],
            Department = accountInfo[5],
            Position = accountInfo[6],
            TelePrefix = accountInfo[7],
            TelePhoneNumber = accountInfo[8],
            Description = accountInfo[3],
            EntityState = SelectedAccount.EntityState,
            CreatedDate = SelectedAccount.CreatedDate is null ? DateTime.Now.ToString("yyyy-MM-dd") : SelectedAccount.CreatedDate
         };

         return receivedInfo;
      }

      private void UpdateAccountInfo(Account accountInfo)
      {
         if(CanSaveUpdatedState)
         {
            accountInfo.EntityState = EntityStateOption.Updated;
         }
         else
         {
            accountInfo.EntityState = EntityStateOption.Inserted;
         }

         accountInfo.UpdatedDate = DateTime.Now.ToString("yyyy-MM-dd");
         accountInfo.TelePrefix = PhoneNumberConverter.ConvertToNumber(accountInfo.TelePrefix);
         accountInfo.FullPhoneNumber = accountInfo.TelePrefix + accountInfo.TelePhoneNumber;

         Accounts.Insert(Accounts.IndexOf(SelectedAccount), accountInfo);
         Accounts.Remove(SelectedAccount);

         CanSaveUpdatedState = true;
         accountInfo = null;
         AccountInfoInit();
      }

      private void AddAccountInfo(Account accountInfo)
      {     
         accountInfo.EntityState = EntityStateOption.Inserted;
         accountInfo.TelePrefix = PhoneNumberConverter.ConvertToNumber(accountInfo.TelePrefix);
         accountInfo.FullPhoneNumber = accountInfo.TelePrefix + accountInfo.TelePhoneNumber;

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
            MessageSend("전화번호", "숫자로 입력해주세요.");
            return false;
         }

         var AccountsToCompare = Accounts;

         //다시 생각해보기
         //foreach (var companyName in AccountsToCompare.Where(x => x.CompanyName == accountInfo.CompanyName)
         //    .Select(same => same.CompanyName))
         //{
         //   if (!string.IsNullOrEmpty(companyName))
         //      return false;
         //}

         //foreach (var telePhoneNumber in AccountsToCompare.Where(x => x.TelePhoneNumber == accountInfo.TelePhoneNumber)
         // .Select(same => same.TelePhoneNumber))
         //{
         //   if (!string.IsNullOrEmpty(telePhoneNumber))
         //      return false;
         //}

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

         if (CheckedResult?.ToLower() == "true" &&
            savedResult.FirstOrDefault() != null &&
            MessageBox.Show($"리스트를 저장하시겠습니까?", "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            result = ButtonResult.OK;
            parameterValue = savedResult;
            SaveDb(savedResult);
         }
         else if (CheckedResult?.ToLower() == "false"
            && savedResult.FirstOrDefault() != null
            )
         {
            if(MessageBox.Show($"저장되지 않은 정보가 있습니다.\n저장하시겠습니까?", "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
               result = ButtonResult.OK;
               parameterValue = savedResult;
               SaveDb(savedResult);
            }
         }

         var parameters = GetDialogParameters(transportParameter, parameterValue);
         RaiseRequestClose(result, parameters);

         Accounts = null;
         InnerAccounts = null;
         SelectedAccount = null;
      }

      private void SaveDb(IEnumerable<Account> savedResult)
      {
        DataService.SendEntityStatus(savedResult);       
      }

      private IEnumerable<Account> AccountListToSave(ObservableCollection<Account> accounts)
      {
         foreach (var item in accounts.Where(account => account.EntityState != EntityStateOption.DBUpdated))
         {
            InnerAccounts.Add(item);
         }

         return InnerAccounts.AsEnumerable();
      }

      private DialogParameters GetDialogParameters(DialogParameters transportParameter, IEnumerable parameterValue)
      {
         if(parameterValue != null)
         {
            foreach (var test in parameterValue)
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

      private void CommandInitialize()
      {
         MoveAccountInfoToListCommand = new DelegateCommand<string>(AddOrUpdate).ObservesCanExecute(() => CanSaveExcute);
         SelectedAccountInfoCommand = new DelegateCommand<object>(DeleteAccount);
         AccountInfoDialogCommand = new DelegateCommand<string>(CloseDialog);
         RevertUpdateInfoCommand = new DelegateCommand(AccountInfoInit);
      }

      private void DataInitialize()
      {
         InnerAccounts = new List<Account>();
         RepogitoryManager = new AccountManager();
         DataService = new AccountService();
         CanSaveUpdatedState = true;

         var result = RepogitoryManager.GetAll();
         Accounts = result != null ? new ObservableCollection<Account>(result) : new ObservableCollection<Account>();

         AccountInfoInit();
      }

      private void AccountInfoInit()
      {
         SelectedAccount = null;
         SelectedAccount = new Account(Newid());
         SelectedAccount.EntityState = EntityStateOption.None;
      }


      #region Dialog Window Resource

      public string Title => "거래처 정보";

      public int DialogWindowWith => 1100;

      #endregion
   }

}
