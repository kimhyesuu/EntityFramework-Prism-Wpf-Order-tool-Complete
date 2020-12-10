using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HS.ERP.Business.Managers
{
   public interface IRepogitoryManager<TEntity>
   {
      ObservableCollection<TEntity> GetAll();
      void AddRange(IEnumerable<TEntity> list);
      bool Add(TEntity account);
      bool Remove(TEntity account);    
      bool Update(TEntity account);
      TEntity Search(TEntity product);
   }
}
