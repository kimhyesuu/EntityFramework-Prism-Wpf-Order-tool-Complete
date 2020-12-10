namespace HS.ERP.Business.Models
{
   using HS.ERP.Business.Models.Enums;
   using System;

   public class OrderProduct
   {
      public OrderProduct()
         => DetailedOrderId = Newid();

      public long? DetailedOrderId { get; set; }
      public long? OrderId { get; set; }
      public long? ProductId { get; set; }
      public string ProductName { get; set; }
      public int? TotalQuantity { get; set; }
      public EntityStateOption EntityState { get; set; }

      private long? Newid()
      {
         var rd = new Random();
         return long.Parse(DateTime.Now.ToString("yyyyMMdd") + rd.Next(1, 1000));
      }
   }
}
