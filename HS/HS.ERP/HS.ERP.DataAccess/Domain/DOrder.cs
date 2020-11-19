namespace HS.ERP.DataAccess.Domain
{
   using System.Collections.Generic;
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("Order")]
   public class DOrder
   {
      [Key]
      [Column("OrderId"), DataType("BIGINT"), DatabaseGenerated(DatabaseGeneratedOption.None)]
      public long? OrderId { get; set; }

      [Column("OrderPrice")]
      public decimal? OrderPrice { get; set; }

      [Column("Description"), MaxLength(800)]
      public string Description { get; set; }

      [Column("CreatedDate"), Timestamp]
      public byte[] CreatedDate { get; set; }

      public long? AccountIdFK { get; set; }
      public DAccount AccountOrdered { get; set; }

      [ForeignKey("OrderIdFK")]
      public virtual ICollection<DOrderProduct> DOrderProduct { get; set; }
   }
}
