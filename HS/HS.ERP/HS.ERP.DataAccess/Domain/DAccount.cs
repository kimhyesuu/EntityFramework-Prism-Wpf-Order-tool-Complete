namespace HS.ERP.DataAccess.Domain
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;

   [Table("AccountInfo")]
   public class DAccountInfo
   {
      [Key]
      [Column("AccountId")]
      public int? AccountId { get; set; }

      [Column("CompanyName")]
      public string CompanyName { get; set; }

      [Index("IX_Company", 1, IsUnique = true)]
      [Column("CompanyEmail"),MaxLength(450)]     
      public string CompanyEmail { get; set; }

      [Index("IX_Company", 2, IsUnique = true)]
      [Column("CompanyPhone"),MaxLength(20)]    
      public string CompanyPhone { get; set; }

      [Column("Address")]
      public string Address { get; set; }

      [Column("Descrption")]
      public string Description { get; set; }

      [Column("CreatedDate"),Timestamp]
      public byte[] CreatedDate { get; set; }

      [Column("UpdatedDate")]
      public DateTime? UpdatedDate { get; set; }
 
      [ForeignKey("AccountIdFK")]
      public virtual ICollection<DOrder> OrderedAccountList { get; set; }
   }

   [Table("Contact")]
   public class DContact
   {
      [Key]
      [Column("ContactId")]
      public int? ContactId { get; set; }

      [Column("AccountId"), Index(IsUnique = true)]
      public int? AccountId { get; set; }
  
      [Column("ContactName"), Required]
      public string ContactName { get; set; }

      [Column("Department")]
      public string Department { get; set; }

      [Column("Position")]
      public string Position { get; set; }

      [Column("PhoneNumber"), Required]
      public string PhoneNumber { get; set; }
   }
}
