using System.Collections.Generic;

namespace HS.ERP.Business.Services
{
   public interface IDataService<TEntity>
   {
      IEnumerable<TEntity> GetAll();
      void SendEntityStatus(IEnumerable<TEntity> parameter);
   }
}
