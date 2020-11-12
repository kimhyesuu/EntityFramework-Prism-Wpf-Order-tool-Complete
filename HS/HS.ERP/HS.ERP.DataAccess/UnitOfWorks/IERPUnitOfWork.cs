namespace HS.ERP.DataAccess.UnitOfWorks
{
   using HS.ERP.DataAccess.Domain;
   using HS.ERP.DataAccess.Repogitory;

   public interface IERPUnitOfWork
   {
      IERPRepogitary<DAccountInfo> Accounts { get; }

      IERPRepogitary<DContact> AccountContacts { get; }

      IERPRepogitary<DProduct> Product { get; }
    
      IERPRepogitary<DOrder> Orders { get; }

      IERPRepogitary<DOrderProduct> OrderProduct { get; }

      void Save();
   }
}
