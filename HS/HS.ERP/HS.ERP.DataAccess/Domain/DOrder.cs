namespace HS.ERP.DataAccess.Domain
{
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("Order")]
   public class DOrder
   {
      [Key]
      [Column("OrderId")]
      public int? OrderId { get; set; }

      [Column("OrderPrice")]
      public int? OrderPrice { get; set; }

      [Column("Status")]
      public bool? Status { get; set; }

      [Column("Description")]
      public string Description { get; set; }

      [Column("CreatedDate"), Timestamp]
      public byte[] CreatedDate { get; set; }

      public int? AccountIdFK { get; set; }
      public DAccountInfo AccountOrdered { get; set; }
   }
}
