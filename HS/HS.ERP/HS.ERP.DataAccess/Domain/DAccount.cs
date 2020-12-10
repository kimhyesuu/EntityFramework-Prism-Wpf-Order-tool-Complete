namespace HS.ERP.DataAccess.Domain
{
   using System.Collections.Generic;
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("Account")]
   public class DAccount
   {
      [Key]
      [Column("AccountId"),DataType("BIGINT"), DatabaseGenerated(DatabaseGeneratedOption.None)]
      public long? AccountId { get; set; }

      [Column("CompanyName"), MaxLength(30),Required]
      public string CompanyName { get; set; }

      [Column("CompanyEmail"), MaxLength(255), Index(IsUnique = true)]     
      public string CompanyEmail { get; set; }

      [Column("Address"), MaxLength(255)]
      public string Address { get; set; }

      [Column("ContactName"), MaxLength(20)]
      [Required]
      public string ContactName { get; set; }

      [Column("Department"), MaxLength(20)]
      public string Department { get; set; }

      [Column("Position"), MaxLength(10)]
      public string Position { get; set; }

      [Column("TelePrefix"), DataType("VARCHAR"), StringLength(3), Required]
      public string TelePrefix { get; set; }

      [Column("TelePhoneNumber"), DataType("VARCHAR"), StringLength(10), Required]
      public string TelePhoneNumber { get; set; }

      [Column("Descrption"), MaxLength(800)]
      public string Description { get; set; }

      [Column("CreatedDate")]
      public string CreatedDate { get; set; }

      [Column("UpdatedDate")]
      public string UpdatedDate { get; set; }
 
      [ForeignKey("AccountIdFK")]
      public virtual ICollection<DOrder> DOrder { get; set; }
   }
}
