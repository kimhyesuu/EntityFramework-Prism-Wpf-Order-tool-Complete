using HS.ERP.Business.Models;
using System.Collections.Generic;

namespace HS.ERP.Repository
{
   public class ProductRepogitory
   {
      private static List<Product> _products;

      public ProductRepogitory()
      {
         _products = new List<Product>();
      }

      public void Add(Product product)
      {
         _products.Add(product);
      }

      public void Remove(Product product)
      {
         _products.Remove(product);
      }

      public Product Search(long? id)
      {
         int index = GetIndex(id);
         if (index > -1)
            return _products[index];
         return null;
      }

      public int GetIndex(long? id)
      {
         int index = -1;

         if (_products.Count > 0)
         {
            for (int i = 0; i < _products.Count; i++)
            {
               if (_products[i].ProductId == id)
               {
                  index = i;
                  break;
               }
            }
         }
         return index;
      }
   }
}
