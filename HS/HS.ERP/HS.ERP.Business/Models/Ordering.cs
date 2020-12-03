namespace HS.ERP.Business.Models
{
   using HS.ERP.Business.Models.Enums;
   using System;

   public class Ordering
   {
      public long? OrderId { get; set; }
      public long? OrderPrice { get; set; }
      public int? OrderQuantity { get; set; }
      public string Description { get; set; }
      public DateTime? CreatedDate { get; set; }
      public EntityStateOption EntityState { get; set; }
   }
}
