using System.Collections.ObjectModel;
using HS.ERP.Business.Models;
using HS.ERP.Core;
using Prism.Events;
using Prism.Mvvm;

namespace Modules.Order.ViewModels
{
   public class OrderedListViewModel : BindableBase
   {
      private ObservableCollection<Account> _accounts;

      public ObservableCollection<Account> Accounts
      {
         get { return _accounts; }
         set { SetProperty(ref _accounts, value); }
      }

      public OrderedListViewModel(IEventAggregator eventAggregator)
      {
         eventAggregator.GetEvent<SendUpdatedList>().Subscribe(ListReceived);
      }

      private void ListReceived(object objList)
      {
         // 여기서 값을 받아내야한다. 
         
         // 1. 제품 것인지 거래처 정보인것인지 확인  
         // 2. 업데이트된 것과 추가한 것과 지운 것 확인
      }
   }
}
