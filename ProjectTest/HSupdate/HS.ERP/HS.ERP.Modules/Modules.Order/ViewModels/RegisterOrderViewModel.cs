using HS.ERP.Core;
using Prism.Mvvm;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Modules.Order.ViewModels
{
   public class RegisterOrderViewModel : BindableBase
   {
      private ObservableCollection<object> _messages;

      private IDataManager<object> DataManager { get; }

      public ObservableCollection<object> Messages
      {
         get { return _messages; }
         set { SetProperty(ref _messages, value); }
      }

      public RegisterOrderViewModel(IDataManager<object> dataManager)
      {
         this.DataManager = dataManager;
         Messages = new ObservableCollection<object>();                
      }
   }
}
