namespace HS.ERP.DataAccess.Domain
{
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("Product")]
   public class DProduct
   {
      [Key]
      [Column("ProductId")]
      public int? ProductId { get; set; }

      [Column("ProductName")]
      public string ProductName { get; set; }

      [Column("ProductPrice")]
      public decimal ProductPrice { get; set; }

      [Column("Description")]
      public string Description { get; set; }

      [Column("CreatedDate"), Timestamp]
      public byte[] CreatedDate { get; set; }
   }
}
