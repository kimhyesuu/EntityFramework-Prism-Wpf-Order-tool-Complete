using System;

namespace HS.ERP.Business.Models
{
   public class Order
   {
      public int? OrderId { get; set; }
      public int? ProductId { get; set; }
      public int? AccountId { get; set; }

      public int? Orderquantity { get; set; }
      public string Description { get; set; }      
      public DateTime CreatedDate { get; set; }
   }
}
