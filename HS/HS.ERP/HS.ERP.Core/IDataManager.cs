using System.Collections.Generic;

namespace HS.ERP.Core
{
   public interface IDataManager<T>
   {
      List<T> GetString { get; }

      bool Add();
      bool Add(T parameter);
      bool AddRange(List<T> parameters);
   }
}