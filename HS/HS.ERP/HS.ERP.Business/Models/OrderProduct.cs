namespace HS.ERP.Business.Models
{
   using HS.ERP.Business.Models.Enums;

   public class OrderProduct
   {
      public long? DetailedOrderId { get; set; }
      public string ProductName { get; set; }
      public int? TotalQuantity { get; set; }
      public EntityStateOption EntityState { get; set; }
   }
}
