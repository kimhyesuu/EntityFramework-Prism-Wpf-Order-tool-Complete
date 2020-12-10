using HS.ERP.Business.Managers;
using HS.ERP.Business.Models;
using HS.ERP.Business.Services;
using HS.ERP.Core;
using Modules.Order.Converters;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Modules.Order.ViewModels
{
   public class RegisterOrderViewModel : BindableBase
   {
      private ObservableCollection<Account> _accounts;
      private ObservableCollection<Product> _products;
      private ObservableCollection<Ordered> _orderedList;
      
      private Product _productInfo;
      private Account _accountInfo;

      private IDataService<Account> AccountService { get; set; }
      private IDataService<Product> ProductService { get; set; }
      private IDataService<Ordered> OrderService { get; set; }

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

      public ObservableCollection<HS.ERP.Business.Models.Ordered> OrderedList
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

      private int? _totalQuantity;
      public int? TotalQuantity
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

      private string _buttonTitle;
      public string AccountButtonTitle
      {
         get { return _buttonTitle; }
         set { SetProperty(ref _buttonTitle, value); }
      }

      public string ProductTitleHeader => "주문";
      public string AccountTitleHeader => "주문";

      public RegisterOrderViewModel(IEventAggregator eventAggregator)
      {
         eventAggregator.GetEvent<SendUpdatedList>().Subscribe(ListReceived);
         SaveInfotemporarilyCommand = new DelegateCommand(MoveOrderList);
         SelectedAccountInfoCommand = new DelegateCommand<Account>(MoveAccountToOrder);
         SelectedProductInfoCommand = new DelegateCommand<Product>(MoveProductToOrder);
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
         OrderService = new OrderService();
         ProductService = new ProductService();
         AccountService = new AccountService();
         AccountManager = new AccountManager();
         ProductManager = new ProductManager();
         CollectionInit();      
      }

      private void AllInfoInit()
      {
         OrderPrice = 0;
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
         }
      }

      private void CollectionInit()
      {
         OrderedList = new ObservableCollection<Ordered>();
         Orderings = new List<Ordering>();
         OrderProducts = new List<OrderProduct>();

         if(DBFlag.Accountflag && DBFlag.Productflag)
         {
            AccountManager.AddRange(AccountService.GetAll());
            ProductManager.AddRange(ProductService.GetAll());
         }

         var accountResult = AccountManager.GetAll();
         var productResult = ProductManager.GetAll();
         Accounts = accountResult is null ? new ObservableCollection<Account>() : new ObservableCollection<Account>(accountResult);
         Products = productResult is null ? new ObservableCollection<Product>() : new ObservableCollection<Product>(productResult);

         DBFlag.UsingMemory();
      }
      
      public DelegateCommand InitCommand { get; private set; }
      public DelegateCommand ConfirmOrderInfoCommand { get; private set; }
      public DelegateCommand CheckingOrderingPriceCommand { get; private set; }
      public DelegateCommand SaveInfotemporarilyCommand { get; private set; }
      public DelegateCommand<Account> SelectedAccountInfoCommand { get; private set; }
      public DelegateCommand<Product> SelectedProductInfoCommand { get; private set; }


      private void MoveAccountToOrder(Account info)     
        => AccountInfo = info;

      private void MoveProductToOrder(Product info)
        => ProductInfo = info;

      private void MoveOrderList()
      {
         if(!IsCompatibility())         
            return;
         
         AddOrderList();
         TotalQuantity = OrderedList.Select(o => o.OrderQuantity).Sum();

         AllInfoInit();
         AccountInit();
      }

      private void SaveDb()
      {
         if (OrderedList.Count > 0 && MessageBox.Show($"리스트를 저장하시겠습니까?", "정보", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
         {
            foreach (var Order in OrderedList)
            {
               Order.TotalQuantity = TotalQuantity;
            }

            OrderService.SendEntityStatus(OrderedList);

            OrderedList = null;
            OrderedList = new ObservableCollection<Ordered>();
            TotalQuantity = 0;
            AllInfoInit();
            AccountInit();
            ProductInit();
         }
      }

      private void AddOrderList()
      {
         OrderedList.Add(new Ordered(Newid())
         {
            ProductId = ProductInfo.ProductId,
            AccountId = AccountInfo.AccountId,           
            ProductName = ProductInfo.ProductName,
            OrderPrice = OrderPrice is null ? OrderQuantity * int.Parse(ProductInfo.ProductPrice) : OrderPrice,
            OrderQuantity = OrderQuantity,
            CompanyName = AccountInfo.CompanyName,
            ContactName = AccountInfo.ContactName,
            FullPhoneNumber = AccountInfo.FullPhoneNumber,
            CreatedDate = DateTime.Now.ToString(),
            Description = Description,
            EntityState = HS.ERP.Business.Models.Enums.EntityStateOption.Inserted
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
         if(objList.FirstOrDefault() is null)
         {
            return;
         }
         else if (objList.FirstOrDefault() is Account)
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

      private long? Newid()
      {
         var rd = new Random();
         return long.Parse(DateTime.Now.ToString("yyyyMMdd") + rd.Next(1, 1000));
      }
   }
}
