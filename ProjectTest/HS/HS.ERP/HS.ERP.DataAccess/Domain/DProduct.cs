namespace HS.ERP.DataAccess.Domain
{
   using System.Collections.Generic;
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("Product")]
   public class DProduct
   {
      [Key]
      [Column("ProductId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int? ProductId { get; set; }

      [Column("ProductName"), MaxLength(50)]
      public string ProductName { get; set; }

      [Column("ProductPrice")]
      public decimal? ProductPrice { get; set; }

      [Column("Description"), MaxLength(800)]
      public string Description { get; set; }

      [Column("CreatedDate")]
      public byte[] CreatedDate { get; set; }

      [ForeignKey("ProductIdFK")]
      public virtual ICollection<DOrderProduct> DOrderProduct { get; set; }
   }
}
