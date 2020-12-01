using HS.ERP.Business.Models;
using HS.ERP.Core;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Modules.Order.ViewModels
{
   public class RegisterOrderViewModel : BindableBase
   {
      private ObservableCollection<Account> _accounts;

      public ObservableCollection<Account> Accounts
      {
         get { return _accounts; }
         set { SetProperty(ref _accounts, value); }
      }

      public RegisterOrderViewModel(IEventAggregator eventAggregator)
      {
         eventAggregator.GetEvent<SendUpdatedList>().Subscribe(ListReceived);
         Accounts = new ObservableCollection<Account>();
      }

      private void ListReceived(IEnumerable<object> objList)
      {
         //오브젝트값을 판별하기 
         // 판별하기 위해서 objlist의 값을 확인해야대
         IEnumerable<Account> a;
         IEnumerable<Product> b;

         if(objList is Account)
         {
            a = objList;
         }
         



         foreach (var Info in list)
         {
            Accounts.Add((Account)accountInfo);
         }
         // var list = highlightedItemProperty.GetValue(objList,null);

         // 여기서 값을 받아내야한다. 

         // 1. 제품 것인지 거래처 정보인것인지 확인  
         // 2. 업데이트된 것과 추가한 것과 지운 것 확인      
      }

      private object GetTypeList(IEnumerable<object> objList)
      {
      }
   }
}
