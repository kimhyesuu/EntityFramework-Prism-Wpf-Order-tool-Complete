namespace HS.ERP.Business.Services
{
   using System.Collections.Generic;

   public interface IServiceLogic<TEntity>
   {
      TEntity Insert(TEntity parameter);
      void Update(TEntity parameter);
      void Delete(object id);
   }
}
