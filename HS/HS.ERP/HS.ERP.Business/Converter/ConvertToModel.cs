using HS.ERP.Business.Models;
using HS.ERP.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HS.ERP.Business.Converter
{
   public static class ConvertToModel
   {
      internal static void ConvertToAccountInfoDomain(Account account, DAccountInfo accountInfo)
      {    
         var rd = new Random();

         account.AccountId = account.AccountId;
         accountInfo.AccountId = account.AccountId;
         accountInfo.CompanyName = account.CompanyName;
         accountInfo.CompanyEmail = account.CompanyEmail;

         accountInfo.CompanyPhone = account.CompanyPhoneNumber[0]
                                   + account.CompanyPhoneNumber[1]
                                   + account.CompanyPhoneNumber[2];       
         
         accountInfo.Address = account.Address;
         accountInfo.Description = account.Description;
      }

      internal static void ConvertToContactDomain(Account account, DContact contact)
      {
         contact.AccountId = account.ContactId;
         contact.ContactId = account.ContactId;
         contact.ContactName = account.ContactName;
         contact.Department = account.Department;
         contact.Position = account.Position;
         contact.PhoneNumber = account.ContactPhoneNumber[0]
                             + account.ContactPhoneNumber[1]
                             + account.ContactPhoneNumber[2];
      }

      internal static IEnumerable<Account> ConvertToClient(IEnumerable<DAccountInfo> accounts, IEnumerable<DContact> contacts)
      {      
         // var result = accounts.Zip(contacts, (i, s) => new { Key = i, Value = s });
         var mergedList = accounts.Zip(contacts, (accountInfo, contactInfo)
            => new
            {
               accountInfo.AccountId,
               accountInfo.CompanyName,
               accountInfo.CompanyPhone,
               accountInfo.CompanyEmail,
               accountInfo.Address,
               accountInfo.Description,
               accountInfo.CreatedDate,
               accountInfo.UpdatedDate,

               contactInfo.ContactId,
               contactInfo.ContactName,
               contactInfo.Department,
               contactInfo.Position,
               contactInfo.PhoneNumber
            });

         var clientAccounts = mergedList.Where(o => o.CompanyName != null).OrderBy(o => o.CompanyName).Select(o => new Account()
         {
            AccountId = o.AccountId,
            CompanyName = o.CompanyName,
            CompanyPhoneNumber = new string[4] { string.Empty, string.Empty, string.Empty, o.CompanyPhone },
            CompanyEmail = o.CompanyEmail,
            Address = o.Address,
            Description = o.Description,
            CreatedDate = o.CreatedDate,
            UpdatedDate = o.UpdatedDate,

            ContactId = o.ContactId,
            ContactName = o.ContactName,
            Department = o.Department,
            Position = o.Position,
            ContactPhoneNumber = new string[4] { string.Empty, string.Empty, string.Empty, o.PhoneNumber }
         }).AsEnumerable();

         return clientAccounts;

      }



      //public static IEnumerable<Account> ConverToClientAccount(IEnumerable<DAccountInfo> accounts, IEnumerable<DContact> contacts)
      //{
      //  // Domain에서 UI로 변환하는 과정
      //}

      //public static IEnumerable<DAccountInfo> ConverToDomainAccountInfo(IEnumerable<Account> accounts)
      //{
      //   //UI에서 Domain로 변환하는 과정
      //   return accounts;
      //}

      //public static IEnumerable<DContact> ConverToDomainContact(IEnumerable<Account> accounts)
      //{
      //   //UI에서 Domain로 변환하는 과정
      //   return accounts;
      //}
   }

   
}
