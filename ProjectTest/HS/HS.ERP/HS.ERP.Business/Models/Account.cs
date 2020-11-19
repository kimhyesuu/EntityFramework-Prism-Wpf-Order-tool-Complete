namespace HS.ERP.Business.Models
{
   using System;
   using System.Collections.ObjectModel;

   public class Account
   {
      //AccountInfo Table
      public int? AccountId { get; set; }

      public string CompanyName { get; set; }

      public string[] CompanyPhoneNumber { get; set; }

      public string CompanyEmail { get; set; }

      public string Address { get; set; }

      public string Description { get; set; }

      public byte[] CreatedDate { get; set; }

      public DateTime? UpdatedDate { get; set; }

      //Contact Table
      public int? ContactId { get; set; }

      public string ContactName { get; set; }

      public string Department { get; set; }

      public string Position { get; set; }

      public string[] ContactPhoneNumber { get; set; }

      public ObservableCollection<Account> Accounts { get; set; }

      public Account()
      {
         CompanyPhoneNumber = new string[4];
         ContactPhoneNumber = new string[4];
         Accounts = new ObservableCollection<Account>();
      }
   }
}
