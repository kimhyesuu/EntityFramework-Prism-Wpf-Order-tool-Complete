using HS.ERP.Business.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HS.ERP.Repository
{
   public class ProductRepogitory
   {
      private static List<Product> _products;

      public ProductRepogitory()      
        => _products = new List<Product>();

      public ObservableCollection<Product> GetAll()
        => new ObservableCollection<Product>(_products);

      public void Add(Product product)      
        => _products.Add(product);
     
      public void Remove(Product product)     
        => _products.Remove(product);

      public void Update(Product product)
      {
         var result = Search(product);

         if (result != null)
         {
            Remove(result);
            Add(product);
         }
      }

      public Product Search(Product product)
      {
         int index = GetIndex(product);
         if (index > -1)
            return _products[index];
         return null;
      }

      public int GetIndex(Product product)
      {
         var index = -1;
         if (_products.Count() > 0)
            index = _products.IndexOf(product);

         return index;
      }
   }
}
