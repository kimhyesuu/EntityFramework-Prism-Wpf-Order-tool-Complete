namespace HS.ERP.Business.Models
{
   public class Product 
   {
      public long? ProductId { get; set; }

      public string ProductName { get; set; }

      public decimal ProductPrice { get; set; }

      public string Description { get; set; }

      public byte[] CreatedDate { get; set; }
   }
}
