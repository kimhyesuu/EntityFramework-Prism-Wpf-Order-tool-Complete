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
      private ObservableCollection<Ordered> _orderedList;
      
      private Product _productInfo;
      private Account _accountInfo;

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

      public List<Ordering> Orderings { get; set; }

      public List<OrderProduct> OrderProducts { get; set; }

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

      private int? _orderPrice;
      public int? OrderPrice
      {
         get { return _orderPrice; }
         set
         {         
            SetProperty(ref _orderPrice, value);
         }
      }

      private int _orderQuantity;
      public int OrderQuantity
      {
         get { return _orderQuantity; }
         set
         {
            SetProperty(ref _orderQuantity, value);
         }
      }

      private int _totalQuantity;
      public int TotalQuantity
      {
         get { return _totalQuantity; }
         set
         {
            SetProperty(ref _totalQuantity, value);
         }
      }

      private string _description;
      public string Description
      {
         get { return _description; }
         set
         {
            SetProperty(ref _description, value);
         }
      }

      public RegisterOrderViewModel(IEventAggregator eventAggregator)
      {
         eventAggregator.GetEvent<SendUpdatedList>().Subscribe(ListReceived);
         SaveInfotemporarilyCommand = new DelegateCommand(MoveOrderList);
         SelectedAccountInfoCommand = new DelegateCommand<Account>(MoveAccountToOrder);
         CheckingOrderingPriceCommand = new DelegateCommand(GetOrderPrice);
         ConfirmOrderInfoCommand = new DelegateCommand(SaveDb);
         InitCommand = new DelegateCommand(AllInfoInit);
         DataInitialize();        
      }

      private void GetOrderPrice()
      {
         if(ProductInfo.ProductPrice != null)
         {
            OrderPrice = int.Parse(ProductInfo.ProductPrice) * OrderQuantity;
         }
      }

      private void DataInitialize()
      {
         AccountInit();
         ProductInit();

         AccountManager = new AccountManager();
         ProductManager = new ProductManager();
         CollectionInit();      
      }

      private void AllInfoInit()
      {
         OrderPrice = null;
         OrderQuantity = 0;
         Description = string.Empty;
      
      }

      private void AccountInit()
      {
         if(AccountInfo != null)
         {
            AccountInfo = null;
            AccountInfo = new Account();
         }
         else
         {
            AccountInfo = new Account();
         }
      }

      private void ProductInit()
      {
         if(ProductInfo != null)
         {
            ProductInfo = null;
            ProductInfo = new Product();
         }   
         else
         {
            ProductInfo = new Product();
            AccountInfo = new Account();
         }
      }

      private void CollectionInit()
      {
         var accountResult = AccountManager.GetAll();
         var productResult = ProductManager.GetAll();
         OrderedList = new ObservableCollection<Ordered>();
         Orderings = new List<Ordering>();
         OrderProducts = new List<OrderProduct>();

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
      
      public DelegateCommand InitCommand { get; private set; }
      public DelegateCommand ConfirmOrderInfoCommand { get; private set; }
      public DelegateCommand CheckingOrderingPriceCommand { get; private set; }
      public DelegateCommand SaveInfotemporarilyCommand { get; private set; }
      public DelegateCommand<Account> SelectedAccountInfoCommand { get; private set; }
      
      private void MoveAccountToOrder(Account info)     
        => AccountInfo = info;
      
      private void MoveOrderList()
      {
         if(!IsCompatibility())         
            return;
         
         AddOrderList();
         TotalQuantity = OrderedList.Select(o => o.OrderdQuantity).Sum();

         AllInfoInit();
         AccountInit();
      }

      private void SaveDb()
      {
         TotalQuantity = 0;
         //여기서 값을 보낸다.
      }

      private void AddOrderList()
      {
         OrderedList.Add(new Ordered
         {
            ProductId = ProductInfo.ProductId,
            ProductName = ProductInfo.ProductName,
            OrderPrice = OrderPrice is null ? OrderQuantity * int.Parse(ProductInfo.ProductPrice) : OrderPrice,
            OrderdQuantity = OrderQuantity,
            CompanyName = AccountInfo.CompanyName,
            ContactName = AccountInfo.ContactName,
            FullPhoneNumber = AccountInfo.FullPhoneNumber,
            CreatedDate = DateTime.Now
         });

         TotalQuantity = OrderedList.Select(mount => mount.OrderdQuantity).Sum();

         Orderings.Add(new Ordering
         {
            OrderPrice = OrderPrice,
            Description = Description,
            CreatedDate = DateTime.Now
         });

         OrderProducts.Add(new OrderProduct
         {
            ProductName = ProductInfo.ProductName,
            TotalQuantity = TotalQuantity
         });
      }

      private bool IsCompatibility()
      {
         if (OrderedList.Count > 0)
         {
            var result = OrderedList.Where(o => o.CompanyName == AccountInfo.CompanyName).FirstOrDefault();
          
            if(result != null)
            {
               return false;
               //값을 찾지말고 값을 더하자
            }

            
         }

         if (string.IsNullOrEmpty(ProductInfo.ProductName) || string.IsNullOrEmpty(AccountInfo.CompanyName))
            return false;
         else if (OrderQuantity == 0)
            return false;

         return true;
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
