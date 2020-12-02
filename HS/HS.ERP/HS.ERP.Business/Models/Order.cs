namespace HS.ERP.Business.Models
{
   using HS.ERP.Business.Models.Enums;
   using System;

   public class Order
   {
      public long? OrderId { get; set; }
      public decimal? OrderPrice { get; set; }
      public string Description { get; set; }
      public DateTime? CreatedDate { get; set; }
      public EntityStateOption EntityState { get; set; }
   }
}
