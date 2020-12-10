namespace HS.ERP.DataAccess.Domain
{
   using System;
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
      public long? OrderPrice { get; set; }

      [Column("OrderQuantity")]
      public int? OrderQuantity { get; set; }

      [Column("Description"), MaxLength(800)]
      public string Description { get; set; }

      [Column("CreatedDate")]
      public string CreatedDate { get; set; }

      public long? AccountIdFK { get; set; }
      public virtual DAccount AccountOrdered { get; set; }

      [ForeignKey("OrderIdFK")]
      public virtual ICollection<DOrderProduct> DOrderProduct { get; set; }
   }
}
