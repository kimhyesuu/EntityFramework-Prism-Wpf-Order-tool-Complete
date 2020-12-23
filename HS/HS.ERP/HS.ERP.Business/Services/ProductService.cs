using HS.ERP.Business.Converter;
using HS.ERP.Business.Models;
using HS.ERP.DataAccess.Domain;
using HS.ERP.DataAccess.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;

namespace HS.ERP.Business.Services
{
   public class ProductService : IDataService<Product>
   {
      IERPUnitOfWork unitOfWork { get; }

      public ProductService()
        => unitOfWork = new ERPUnitOfWork(DBConnect.ConnectString);

      public IEnumerable<Product> GetAll()     
        => ConvertToModel.DomainToClient(unitOfWork.Product.GetAll(), unitOfWork.ProductSpec.GetAll());
      
      public void SendEntityStatus(IEnumerable<Product> accounts)
      {
         if (accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted).FirstOrDefault() != null)
         {
            var list = new List<DProduct>();
            var spec = new List<DProductSpec>();

            foreach (var info in accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted))
            {
               list.Add(ConvertToModel.ClientToDomain(info));
               spec.Add(ConvertToModel.SpecClientToDomain(info));
            }

            unitOfWork.Product.Insert(list);
            unitOfWork.ProductSpec.Insert(spec);
         }

         if (accounts.Where(deleteInfo => deleteInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted).FirstOrDefault() != null)
         {
            var list = new List<long?>();

            foreach (var info in accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted))
            {
               list.Add(info.ProductId);
            }

            unitOfWork.ProductSpec.Delete(list);
            unitOfWork.Product.Delete(list);
         }

         if (accounts.Where(updateInfo => updateInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Updated).FirstOrDefault() != null)
         {
            var list = new List<DProduct>();
            var spec = new List<DProductSpec>();

            foreach (var info in accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Updated))
            {
               list.Add(ConvertToModel.ClientToDomain(info));
               spec.Add(ConvertToModel.SpecClientToDomain(info));
            }

            unitOfWork.Product.Update(list);
            unitOfWork.ProductSpec.Update(spec);
         }
      }

   }
}
