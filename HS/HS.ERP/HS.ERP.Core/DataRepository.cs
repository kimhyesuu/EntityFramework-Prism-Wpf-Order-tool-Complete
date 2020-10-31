using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS.ERP.Core
{
   public struct DataRepository<T>
   {
      public static List<T> Objs = new List<T>();

      internal void Add(T parameter)
      {
         Objs.Add(parameter);
      }

      internal void AddRange(List<T> parameters)
      {
         Objs = parameters.ToList<T>();
      }
   }
}
