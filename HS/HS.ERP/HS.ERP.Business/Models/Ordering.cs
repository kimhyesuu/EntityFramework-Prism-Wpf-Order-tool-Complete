namespace HS.ERP.Business.Models
{
   using System;

   public class Ordering
   {
      public Ordering()      
         => OrderId = Newid();
      
      public long? OrderId { get; set; }
      public long? OrderPrice { get; set; }
      public int OrderQuantity { get; set; }
      public string Description { get; set; }
      public DateTime CreatedDate { get; set; }

      private long? Newid()
      {
         var rd = new Random();
         return long.Parse(DateTime.Now.ToString("yyyyMMdd") + rd.Next(1, 1000));
      }
   }
}
