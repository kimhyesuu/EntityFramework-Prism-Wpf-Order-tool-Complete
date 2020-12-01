namespace HS.ERP.DataAccess.Domain
{
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("OrderProduct")]
   public class DOrderProduct
   {
      [Key]
      [Column("DetailedOrderId"), DataType("BIGINT"), DatabaseGenerated(DatabaseGeneratedOption.None)]
      public long? DetailedOrderId { get; set; }
   
      [Column("ProductName")]
      public string ProductName { get; set; }

      [Column("ProductQuantity")]
      public int ProductQuantity { get; set; }

      public long? OrderIdFK { get; set; }
      public DOrder DOrder { get; set; }

      public int? ProductIdFK { get; set; }
      public DProduct DProduct { get; set; }
   }
}
