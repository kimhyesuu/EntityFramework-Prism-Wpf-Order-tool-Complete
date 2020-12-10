namespace HS.ERP.DataAccess.Domain
{
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("OrderProduct")]
   public class DOrderProduct
   {
      [Key]
      [Column("DetailedOrderId"), DataType("BIGINT"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public long? DetailedOrderId { get; set; }
   
      [Column("ProductName")]
      public string ProductName { get; set; }

      [Column("ProductQuantity")]
      public int? TotalQuantity { get; set; }

      public long? OrderIdFK { get; set; }
      public virtual DOrder DOrder { get; set; }

      public long? ProductIdFK { get; set; }
      public virtual DProduct DProduct { get; set; }
   }
}
