using HS.ERP.Business.Models;
using HS.ERP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS.ERP.Business.Managers
{
   public class ProductManager
   {
      readonly ProductRepogitory _repository;

      public ProductManager()
      {
         //여기서 모든 값을 받자 
         this._repository = InfoList.GetCurrentProducts;
      }

      public bool Add(Product product)
      {
         if (_repository.Search(product.ProductId) is null)
         {
            _repository.Add(product);
            return true;
         }
         return false;
      }

      public bool Remove(long? id)
      {
         var product = _repository.Search(id);

         if (product != null)
         {
            _repository.Remove(product);
            return true;
         }
         return false;
      }

      public Product Search(int id)
      {
         return _repository.Search(id);
      }

      //여기서 값 업데이트하자 킹정이네
   }
}
