using HS.ERP.Business.Models;
using HS.ERP.Business.UnitOfWorks;
using System.Collections.Generic;

namespace HS.ERP.Business.BLL
{
   public class ProductService : IDataService<ProductInfo>
   {
      IUnitOfWork unitOfWork { get; }

      public ProductService()
      {
         unitOfWork = new UnitOfWork();
      }

      public IEnumerable<ProductInfo> GetAll()
      {
         return unitOfWork.Products.GetAll();
      }

      public ProductInfo Insert(ProductInfo ProductInfo)
      {
         var AddProduct = unitOfWork.Products.Insert(ProductInfo);
  
         return AddProduct;
      }

      public void Update(ProductInfo ProductInfo)
      {
         unitOfWork.Products.Update(ProductInfo);
      
      }

      public void Delete(ProductInfo ProductInfo)
      {
         unitOfWork.Products.Delete(ProductInfo);
        
      }
   }
}
