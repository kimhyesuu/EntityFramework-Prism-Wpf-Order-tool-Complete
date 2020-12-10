using HS.ERP.Business.Models;
using HS.ERP.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS.ERP.Business.Managers
{
   public class ProductManager : IRepogitoryManager<Product>
   {
      readonly ProductRepogitory _repository;

      public ProductManager()
      {
         this._repository = InfoList.GetCurrentProducts;
      }

      public ObservableCollection<Product> GetAll()
      {
         var result = _repository.GetAll();

         if (result.FirstOrDefault() != null)
         {
            return _repository.GetAll();
         }

         return null;
      }

      public void AddRange(IEnumerable<Product> list)
      {
         foreach (var productInfo in list)
         {
            _repository.Add(productInfo);
         }
      }

      public bool Add(Product product)
      {
         if (_repository.Search(product) is null)
         {
            _repository.Add(product);
            return true;
         }
         return false;
      }

      public bool Remove(Product product)
      {

         if (_repository.Search(product) != null)
         {
            _repository.Remove(product);
            return true;
         }
         return false;
      }

      public bool Update(Product product)
      {
         if (_repository.Search(product) is null)
         {
            _repository.Update(product);
            return true;
         }

         return false;
      }

      public Product Search(Product product)
      {
         return _repository.Search(product);
      }
   }
}
