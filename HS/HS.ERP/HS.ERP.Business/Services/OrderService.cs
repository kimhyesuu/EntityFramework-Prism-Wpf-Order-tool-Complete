using HS.ERP.Business.Converter;
using HS.ERP.Business.Models;
using HS.ERP.DataAccess.Domain;
using HS.ERP.DataAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HS.ERP.Business.Services
{
   public class OrderService : IDataService<Ordered>
   {
      IERPUnitOfWork unitOfWork { get; }

      public OrderService()
        => unitOfWork = new ERPUnitOfWork(DBConnect.ConnectString);

      public IEnumerable<Ordered> GetAll()
        => ConvertToModel.DomainToClient(unitOfWork.Orders.GetAll(), unitOfWork.OrderProduct.GetAll(),unitOfWork.Accounts.GetAll());

      public void SendEntityStatus(IEnumerable<Ordered> orders)
      {
         if (orders.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted).FirstOrDefault() != null)
         {
            var list = new List<DOrder>();
            var orderProducts = new List<DOrderProduct>();

            foreach (var info in orders.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted))
            {
               list.Add(ConvertToModel.ClientToDomain(info));
               orderProducts.Add(ConvertToModel.OrderDetailClientToDomain(info));
            }

            unitOfWork.Orders.Insert(list);
            unitOfWork.OrderProduct.Insert(orderProducts);
         }

         //if (accounts.Where(deleteInfo => deleteInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted).FirstOrDefault() != null)
         //{
         //   var list = new List<long?>();

         //   foreach (var info in accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted))
         //   {
         //      list.Add(info.ProductId);
         //   }

         //   unitOfWork.Product.Delete(list);
         //   unitOfWork.ProductSpec.Delete(list);
         //}

       
      }
   }
}
