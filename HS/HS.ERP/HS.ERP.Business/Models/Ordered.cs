using HS.ERP.Business.Models.Enums;
using System;

namespace HS.ERP.Business.Models
{
   public class Ordered
   {
      public Ordered(long? id)
        => this.OrderId = id;

      public int? SequentialNumber { get; set; }
      public long? ProductId { get; set; }

      public string ProductName { get; set; }
      public long? OrderPrice { get; set; }
      public int? TotalQuantity { get; set; }
      public string CompanyName { get; set; }
      public string ContactName { get; set; }
      public string FullPhoneNumber { get; set; }
      public string CreatedDate { get; set; }

      public long? OrderId { get; set; }

      public long? AccountId { get; set; }
      public int? OrderQuantity { get; set; }
      public string Description { get; set; }
      public EntityStateOption EntityState { get; set; }

   }
}
