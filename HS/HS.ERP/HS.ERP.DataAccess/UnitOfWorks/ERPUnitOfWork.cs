namespace HS.ERP.DataAccess.UnitOfWorks
{
   using HS.ERP.DataAccess.DbContexts;
   using HS.ERP.DataAccess.Domain;
   using HS.ERP.DataAccess.Repogitory;

   public class ERPUnitOfWork : IERPUnitOfWork
   {
      private ERPRepogitary<DAccountInfo> _account;
      private ERPRepogitary<DContact> _Contact;
      private ERPRepogitary<DOrder> _order;
      private ERPRepogitary<DProduct> _product;
      private ERPRepogitary<DOrderProduct> _orderProduct;

      private HSERPEntities _hSERPEntities;

      public ERPUnitOfWork() => _hSERPEntities = new HSERPEntities();

      public IERPRepogitary<DAccountInfo> Accounts => _account ?? (_account = new ERPRepogitary<DAccountInfo>(_hSERPEntities));

      public IERPRepogitary<DContact> AccountContacts => _Contact ?? (_Contact = new ERPRepogitary<DContact>(_hSERPEntities));

      public IERPRepogitary<DProduct> Product => _product ?? (_product = new ERPRepogitary<DProduct>(_hSERPEntities));

      public IERPRepogitary<DOrder> Orders => _order ?? (_order = new ERPRepogitary<DOrder>(_hSERPEntities));

      public IERPRepogitary<DOrderProduct> OrderProduct
         => _orderProduct ?? (_orderProduct = new ERPRepogitary<DOrderProduct>(_hSERPEntities));

      public void Save() => _hSERPEntities.SaveChanges();
   }
}
