using HS.ERP.Business.Managers;
using HS.ERP.Business.Models;
using HS.ERP.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Modules.Order.ViewModels
{
   public class RegisterOrderViewModel : BindableBase
   {
      //SelectedAccountInfoCommand
      private ObservableCollection<Account> _accounts;
      private ObservableCollection<Product> _products;
      private ObservableCollection<Ordered> _orderedList;//이친구를 따로 배치하는 게 맞지
      private Product _productInfo;
      private Account _accountInfo;
      private Ordering _order;
      private OrderProduct _orderProduct;

      private IRepogitoryManager<Account> AccountManager { get; set; }
      private IRepogitoryManager<Product> ProductManager { get; set; }

      public ObservableCollection<Account> Accounts
      {
         get => _accounts; 
         set => SetProperty(ref _accounts, value); 
      }

      public ObservableCollection<Product> Products
      {
         get => _products; 
         set => SetProperty(ref _products, value); 
      }

      public ObservableCollection<Ordered> OrderedList
      {
         get { return _orderedList; }
         set { SetProperty(ref _orderedList, value); }
      }

      public Product ProductInfo
      {
         get => _productInfo; 
         set => SetProperty(ref _productInfo, value); 
      }

      public Account AccountInfo
      {
         get => _accountInfo;
         set => SetProperty(ref _accountInfo, value);
      }

      public Ordering Order
      {
         get { return _order; }
         set { SetProperty(ref _order, value); }
      }

      private int? _orderPrice;
      public int? OrderPrice
      {
         get { return _orderPrice; }
         set
         {         
            SetProperty(ref _orderPrice, value);
         }
      }

      private int? _totalQuantity;
      public int? TotalQuantity
      {
         get { return _totalQuantity; }
         set
         {
            SetProperty(ref _totalQuantity, value);
         }
      }

      public OrderProduct OrderProduct
      {
         get { return _orderProduct; }
         set { SetProperty(ref _orderProduct, value); }
      }

      public RegisterOrderViewModel(IEventAggregator eventAggregator)
      {
         eventAggregator.GetEvent<SendUpdatedList>().Subscribe(ListReceived);
         SaveInfotemporarilyCommand = new DelegateCommand(MoveOrderList);
         SelectedAccountInfoCommand = new DelegateCommand<Account>(MoveAccountToOrder);
         CheckingOrderingPriceCommand = new DelegateCommand(GetOrderPrice);
         DataInitialize();        
      }

      private void GetOrderPrice()
         => OrderPrice = int.Parse(ProductInfo.ProductPrice) * Order.OrderQuantity;

      private void DataInitialize()
      {
         Order = new Ordering();
         OrderProduct = new OrderProduct();
         ProductInfo = new Product();
         AccountInfo = new Account();
         AccountManager = new AccountManager();
         ProductManager = new ProductManager();
         CollectionInit();      
      }

      private void CollectionInit()
      {
         var accountResult = AccountManager.GetAll();
         var productResult = ProductManager.GetAll();
         OrderedList = new ObservableCollection<Ordered>();
         if (accountResult != null)
         {
            Accounts = new ObservableCollection<Account>(accountResult);
         }
         else
         {
            Accounts = new ObservableCollection<Account>();
         }

         if (productResult != null)
         {
            Products = new ObservableCollection<Product>(productResult);
         }
         else
         {
            Products = new ObservableCollection<Product>();
         }
      }
      
      public DelegateCommand CheckingOrderingPriceCommand { get; private set; }
      public DelegateCommand SaveInfotemporarilyCommand { get; private set; }
      public DelegateCommand<Account> SelectedAccountInfoCommand { get; private set; }

      private void MoveAccountToOrder(Account info)     
        => AccountInfo = info;

      public int seq = 0; 
      private void MoveOrderList()
      {
         //정합성 체크        
         OrderedList.Add( new Ordered() {
            SequentialNumber = seq++,
            ProductId = ProductInfo.ProductId,
            ProductName = ProductInfo.ProductName,
            OrderPrice = OrderPrice,         
            OrderdQuantity = Order.OrderQuantity,           
            CompanyName = AccountInfo.CompanyName,
            ContactName = AccountInfo.ContactName,
            FullPhoneNumber = AccountInfo.FullPhoneNumber,
            CreatedDate = DateTime.Now
         });

         TotalQuantity += Order.OrderQuantity;
         ProductInfo = null;
         ProductInfo = new Product();
      }


      private void ListReceived(IEnumerable<object> objList)
      {
         if (objList.First() is Account)
         {
            var accountList = new List<Account>();

            foreach (var info in objList)
            {
               accountList.Add((Account)info);
            }

            if(accountList.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted).FirstOrDefault() != null)
            {
               foreach (var info in accountList.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted))
               {
                  if (AccountManager.Add(info))
                  {
                     info.EntityState = HS.ERP.Business.Models.Enums.EntityStateOption.DBUpdated;
                     Accounts.Add(info);
                  }
               }
            }

            if(accountList.Where(deleteInfo => deleteInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted).FirstOrDefault() !=null)
            {
               foreach (var info in accountList.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted))
               {
                  if (AccountManager.Remove(info))
                  {
                     Accounts.Remove(info);
                  }
               }
            }

            if (accountList.Where(updateInfo => updateInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Updated).FirstOrDefault() != null)
            {
               foreach (var info in accountList.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Updated))
               {
                  if (AccountManager.Update(info))
                  {
                     info.EntityState = HS.ERP.Business.Models.Enums.EntityStateOption.DBUpdated;
                     var result = Accounts.Where(o => o.AccountId == info.AccountId).First();
                     Accounts.Insert(Accounts.IndexOf(result), info);
                     Accounts.Remove(result);
                  }
               }
            }
         }
         else
         {
            var productList = new List<Product>();

            foreach (var info in objList)
            {
                productList.Add((Product)info);
            }

            if (productList.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted).FirstOrDefault() != null)
            {
               foreach (var info in productList.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted))
               {
                  if (ProductManager.Add(info))
                  {
                     Products.Add(info);
                  }
               }
            }

            if (productList.Where(deleteInfo => deleteInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted).FirstOrDefault() != null)
            {
               foreach (var info in productList.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted))
               {
                  if (ProductManager.Remove(info))
                  {
                     info.EntityState = HS.ERP.Business.Models.Enums.EntityStateOption.DBUpdated;
                     productList.Remove(info);
                  }
               }
            }

            if (productList.Where(updateInfo => updateInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Updated).FirstOrDefault() != null)
            {
               foreach (var info in productList.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Updated))
               {
                  if (ProductManager.Update(info))
                  {
                     info.EntityState = HS.ERP.Business.Models.Enums.EntityStateOption.DBUpdated;
                     var result = Products.Where(o => o.ProductId == info.ProductId).First();
                     Products.Insert(Products.IndexOf(result), info);
                     Products.Remove(result);
                  }
               }
            }
         }
      } 
   }
}
