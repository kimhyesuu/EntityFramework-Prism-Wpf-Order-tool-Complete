namespace HS.ERP.DataAccess.Domain
{
   using System.Collections.Generic;
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("Product")]
   public class DProduct
   {
      [Key]
      [Column("ProductId"), DatabaseGenerated(DatabaseGeneratedOption.None)]
      public long? ProductId { get; set; }

      [Column("ProductName"), MaxLength(50)]
      public string ProductName { get; set; }

      [Column("ProductPrice")]
      public int? ProductPrice { get; set; }

      [Column("CreatedDate")]
      public string CreatedDate { get; set; }

      [Column("UpdatedDate")]
      public string UpdatedDate { get; set; }

      [ForeignKey("ProductIdFK")]
      public virtual ICollection<DOrderProduct> DOrderProduct { get; set; }

      [ForeignKey("ProductIdFK")]
      public virtual ICollection<DProductSpec> DProductSpec { get; set; }
   }
}
