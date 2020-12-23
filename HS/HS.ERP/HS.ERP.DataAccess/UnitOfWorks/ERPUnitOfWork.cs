namespace HS.ERP.DataAccess.UnitOfWorks
{
   using HS.ERP.DataAccess.Domain;
   using HS.ERP.DataAccess.Repogitory;

   public class ERPUnitOfWork : IERPUnitOfWork
   {
      private ERPRepogitary<DAccount> _account;
      private ERPRepogitary<DOrder> _order;
      private ERPRepogitary<DProduct> _product;
      private ERPRepogitary<DProductSpec> _productSpec;
      private ERPRepogitary<DOrderProduct> _orderProduct;

      private string _connectString = string.Empty;
         
      public ERPUnitOfWork(string connectString)      
       =>  this._connectString = connectString;
      

      public IERPRepogitary<DAccount> Accounts => _account ?? (_account = new ERPRepogitary<DAccount>(_connectString));

      public IERPRepogitary<DProduct> Product => _product ?? (_product = new ERPRepogitary<DProduct>(_connectString));

      public IERPRepogitary<DProductSpec> ProductSpec => _productSpec ?? (_productSpec = new ERPRepogitary<DProductSpec>(_connectString));

      public IERPRepogitary<DOrder> Orders => _order ?? (_order = new ERPRepogitary<DOrder>(_connectString));

      public IERPRepogitary<DOrderProduct> OrderProduct => _orderProduct ?? (_orderProduct = new ERPRepogitary<DOrderProduct>(_connectString));
   }
}
