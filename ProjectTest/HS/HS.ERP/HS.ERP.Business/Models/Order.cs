namespace HS.ERP.Business.Models
{//고치기
   public class Order
   {
      public long? OrderId { get; set; }

      public decimal? OrderPrice { get; set; }

      public string Description { get; set; }

      public byte[] CreatedDate { get; set; }
   }
}
