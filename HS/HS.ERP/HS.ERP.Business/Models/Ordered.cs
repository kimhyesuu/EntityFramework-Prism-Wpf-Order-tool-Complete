namespace HS.ERP.Business.Models
{
   using System;

   public class Ordered
   {
    
      public int? SequentialNumber { get; set; }
      public long? ProductId { get; set; }
      public string ProductName { get; set; }
      public long? OrderPrice { get; set; }
      public int? TotalQuantity { get; set; }
      public long? OrderdQuantity { get; set; }
      public string CompanyName { get; set; }
      public string ContactName { get; set; }
      public string FullPhoneNumber { get; set; }
      public DateTime? CreatedDate { get; set; } 
   }
}
