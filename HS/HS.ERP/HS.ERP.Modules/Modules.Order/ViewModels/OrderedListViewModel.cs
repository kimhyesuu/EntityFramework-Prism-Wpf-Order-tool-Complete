using HS.ERP.Business.Models;
using HS.ERP.Core;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Modules.Order.ViewModels
{
   public class OrderedListViewModel : BindableBase
   {
      public AccountInfo accountInfo { get; set; }
      private ObservableCollection<AccountInfo> _accountList;

      private IDataManager<AccountInfo> DataManager { get; }

      public ObservableCollection<AccountInfo> AccountList
      {
         get { return accountInfo.AccountList; }
         set { SetProperty(ref accountInfo.AccountList, value); }
      }

      public OrderedListViewModel(IDataManager<AccountInfo> dataManager)
      {
         this.DataManager = dataManager;
         AccountList = new ObservableCollection<AccountInfo>(DataManager.GetString);  
      }
   }
}
