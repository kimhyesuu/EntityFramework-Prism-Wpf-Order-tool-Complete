using System.Collections.ObjectModel;

namespace HS.ERP.Business.Managers
{
   public interface IRepogitoryManager<TEntity>
   {
      ObservableCollection<TEntity> GetAll();
      bool Add(TEntity account);
      bool Remove(TEntity account);    
      bool Update(TEntity account);
      TEntity Search(TEntity product);
   }
}
