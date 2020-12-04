namespace HS.ERP.Business.Models
{
   using System;

   public class OrderProduct
   {
      public OrderProduct()
         => DetailedOrderId = Newid();

      public long? DetailedOrderId { get; set; }
      public string ProductName { get; set; }
      public int? TotalQuantity { get; set; }

      private long? Newid()
      {
         var rd = new Random();
         return long.Parse(DateTime.Now.ToString("yyyyMMdd") + rd.Next(1, 1000));
      }
   }
}
