namespace HS.ERP.Business.Models
{
   using System;

   public class Order
   {
      public long? OrderId { get; set; }
      public decimal? OrderPrice { get; set; }
      public string Description { get; set; }
      public DateTime? CreatedDate { get; set; }
   }
}
