namespace HS.ERP.Business.Models
{
   public class OrderProduct
   {
      public long? DetailedOrderId { get; set; }

      public long? ProductId { get; set; }

      public long? OrderId { get; set; }

      public string ProductName { get; set; }

      public int ProductQuantity { get; set; }
   }
}
