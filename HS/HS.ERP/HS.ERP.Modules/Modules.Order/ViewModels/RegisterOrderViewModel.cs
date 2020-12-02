using HS.ERP.Business.Managers;
using HS.ERP.Business.Models;
using HS.ERP.Core;
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
      private ObservableCollection<Account> _accounts;
      private ObservableCollection<Product> _products;

      public IRepogitoryManager<Account> AccountManager { get; set; }
      public IRepogitoryManager<Product> ProductManager { get; set; }

      public ObservableCollection<Account> Accounts
      {
         get { return _accounts; }
         set { SetProperty(ref _accounts, value); }
      }

      public ObservableCollection<Product> Products
      {
         get { return _products; }
         set { SetProperty(ref _products, value); }
      }

      public RegisterOrderViewModel(IEventAggregator eventAggregator)
      {
         eventAggregator.GetEvent<SendUpdatedList>().Subscribe(ListReceived);
         DataInitialize();        
      }

      private void DataInitialize()
      {
         AccountManager = new AccountManager();
         ProductManager = new ProductManager();
         CollectionInit();      
      }

      private void CollectionInit()
      {
         var accountResult = AccountManager.GetAll();
         var productResult = ProductManager.GetAll();

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
