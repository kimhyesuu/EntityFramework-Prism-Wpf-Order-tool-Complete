namespace HS.ERP.Business.Models
{
   using HS.ERP.Business.Models.Enums;
   using System;

   public class Account
   {
      public Account() { }
   
      public Account(long? id)
         => this.AccountId = id;

      public long? AccountId { get; set; }
      public string CompanyName { get; set; }
      public string CompanyEmail { get; set; }
      public string Address { get; set; }
      public string ContactName { get; set; }
      public string Department { get; set; }
      public string Position { get; set; }
      public string TelePrefix { get; set; }
      public string TelePhoneNumber { get; set; }
      public string FullPhoneNumber { get; set; }
      public string Description { get; set; }
      public byte[] CreatedDate { get; set; }
      public DateTime? UpdatedDate { get; set; }
      public EntityStateOption EntityState { get; set; }

   }
}
