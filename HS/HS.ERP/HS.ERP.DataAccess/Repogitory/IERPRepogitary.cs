namespace HS.ERP.DataAccess.Repogitory
{
   using System.Collections.Generic;

   public interface IERPRepogitary<TEntity>
   {
      IEnumerable<TEntity> GetAll();
      TEntity GetById(object id);
      void Insert(List<TEntity> parameters);
      void Update(List<TEntity> parameters);
      void Delete(List<long?> parameters);
   }
}
