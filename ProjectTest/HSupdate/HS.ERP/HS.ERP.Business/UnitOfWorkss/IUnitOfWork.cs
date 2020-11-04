namespace HS.ERP.Business.UnitOfWorks
{
   using HS.ERP.Business.Models;
   using HS.ERP.DataAccess.Repogitory;

   public interface IUnitOfWork 
   {
      IAccountRepogitory<AccountInfo> Accounts { get; }

      IAccountRepogitory<ProductInfo> Products { get; }

      IAccountRepogitory<Order> Orders { get; }
   }
}
