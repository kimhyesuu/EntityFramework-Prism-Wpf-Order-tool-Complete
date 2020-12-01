namespace HS.ERP.Business.Models
{
   public class DOrder
   {
      public long? OrderId { get; set; }

      public long? AccountId { get; set; }

      public int? OrderPrice { get; set; }

      public bool? Status { get; set; }

      public string Description { get; set; }

      public byte[] CreatedDate { get; set; }
   }
}
