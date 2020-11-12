namespace HS.ERP.Business.Services
{
   using System.Collections.Generic;

   public interface IServiceLogic<TEntity>
   {
      IEnumerable<TEntity> GetAll();
      TEntity Insert(TEntity parameter);
      void Update(TEntity parameter);
      void Delete(TEntity parameter);
   }
}
