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


        public bool AddRange(IEnumerable<T> parameters)
        {
            var para = parameters.ToList<T>();

            if (para != null)
            {
                Repository.Add(para);
                return true;
            }
            return false;
        }

        #endregion

    }


}
