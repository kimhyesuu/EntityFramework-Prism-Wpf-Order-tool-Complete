using System.Collections.Generic;
using System.Linq;

namespace HS.ERP.Core
{
   public class DataManager<T> : IDataManager<T>
   {
      public DataRepository<T> Repository { get; }

      public List<T> GetString { get => DataRepository<T>.Objs; }

      public DataManager()
      {
         Repository = new DataRepository<T>();
      }

      #region Add 

      public bool Add() => false;

      public bool Add(T parameter)
      {
         var para = parameter;

         if (parameter != null)
         {
            Repository.Add(parameter);
            return true;
         }

         return false;
      }

      public bool AddRange(List<T> parameters)
      {
         var para = new List<T>(parameters);

         if (para != null)
         {
            Repository.AddRange(para);
            return true;
         }
         return false;
      }

      #endregion

   }


}
