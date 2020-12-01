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

      private string _connectString = string.Empty;
         
      public ERPUnitOfWork(string connectString)
      {
         this._connectString = connectString;
      }

      public IERPRepogitary<DAccountInfo> Accounts => _account ?? (_account = new ERPRepogitary<DAccountInfo>(_connectString));

      public IERPRepogitary<DContact> AccountContacts => _Contact ?? (_Contact = new ERPRepogitary<DContact>(_connectString));

      public IERPRepogitary<DProduct> Product => _product ?? (_product = new ERPRepogitary<DProduct>(_connectString));

      public IERPRepogitary<DOrder> Orders => _order ?? (_order = new ERPRepogitary<DOrder>(_connectString));

      public IERPRepogitary<DOrderProduct> OrderProduct
         => _orderProduct ?? (_orderProduct = new ERPRepogitary<DOrderProduct>(_connectString));

  

   }
}
