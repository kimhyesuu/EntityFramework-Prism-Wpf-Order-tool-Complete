namespace HS.ERP.DataAccess.Domain
{
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("OrderProduct")]
   public class DOrderProduct
   {
      [Key]
      [Column("DetailedOrderId")]
      public int? DetailedOrderId { get; set; }

      [Column("ProductId")]
      public int? ProductId { get; set; }

      [Column("OrderId")]
      public int? OrderId { get; set; }

      [Column("ProductName")]
      public string ProductName { get; set; }

      [Column("ProductQuantity")]
      public int ProductQuantity { get; set; }
   }
}
