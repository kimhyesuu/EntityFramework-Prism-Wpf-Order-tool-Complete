using HS.ERP.Business.Models;
using HS.ERP.DataAccess.ERPDbContext;
using HS.ERP.DataAccess.Repogitory;

namespace HS.ERP.Business.UnitOfWorks
{
   public class UnitOfWork : IUnitOfWork
   {
      private AccountRepogitory<AccountInfo> _accounts;
      private AccountRepogitory<ProductInfo> _products;
      private AccountRepogitory<Order> _orders;

      private ERPDemoEntities _eRPDemoEntities;

      public UnitOfWork()
      {
         _eRPDemoEntities = new ERPDemoEntities();
      }

      public UnitOfWork(ERPDemoEntities eRPDemoEntities)
      {
         this._eRPDemoEntities = eRPDemoEntities;
      }

      public IAccountRepogitory<AccountInfo> Accounts
      {
         get => _accounts?? (_accounts = new AccountRepogitory<AccountInfo>(_eRPDemoEntities));
      }

      public IAccountRepogitory<ProductInfo> Products
      {
         get => _products ?? (_products = new AccountRepogitory<ProductInfo>(_eRPDemoEntities));
      }

      public IAccountRepogitory<Order> Orders
      {
         get => _orders ?? (_orders = new AccountRepogitory<Order>(_eRPDemoEntities));
      }
   }
}
