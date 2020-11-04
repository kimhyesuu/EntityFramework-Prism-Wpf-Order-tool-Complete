using HS.ERP.Business.Models;
using HS.ERP.Business.UnitOfWorks;
using System.Collections.Generic;

namespace HS.ERP.Business.BLL
{
   public class OrderService : IDataService<Order>
   {
      IUnitOfWork unitOfWork { get; }

      public OrderService()
      {
         unitOfWork = new UnitOfWork();
      }

      public IEnumerable<Order> GetAll()
      {
         return unitOfWork.Orders.GetAll();
      }

      public Order Insert(Order Order)
      {
         var AddOrder = unitOfWork.Orders.Insert(Order);
    
         return AddOrder;
      }

      public void Update(Order Order)
      {
         unitOfWork.Orders.Update(Order);
      
      }

      public void Delete(Order Order)
      {
         unitOfWork.Orders.Delete(Order);
        
      }
   }
}
