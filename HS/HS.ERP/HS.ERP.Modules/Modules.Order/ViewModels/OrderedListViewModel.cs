using System.Collections.ObjectModel;
using HS.ERP.Business.Models;
using HS.ERP.Core;
using Prism.Events;
using Prism.Mvvm;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Collections;
using Prism.Commands;
using HS.ERP.Business.Services;

namespace Modules.Order.ViewModels
{
   public class OrderedListViewModel : BindableBase
   {
      private IDataService<Ordered> dataService;

      private ObservableCollection<Ordered> _orderList;
      public ObservableCollection<Ordered> OrderList
      {
         get { return _orderList; }
         set { SetProperty(ref _orderList, value); }
      }

      public OrderedListViewModel()
      {
         dataService = new OrderService();
         SearchCommand = new DelegateCommand(SearchOrderList);
      }

      private void SearchOrderList()
      {
         OrderList = new ObservableCollection<Ordered>(dataService.GetAll());
      }

      public DelegateCommand SearchCommand { get; private set; }
   }
}
