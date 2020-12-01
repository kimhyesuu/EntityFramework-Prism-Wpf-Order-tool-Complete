using HS.ERP.Business.Models;
using HS.ERP.DataAccess.Domain;
using System.Collections.Generic;
using System.Linq;

namespace HS.ERP.Business.Converter
{
   public static class ConvertToModel
   {
      internal static void ConvertToAccountInfoDomain(Account account, DAccountInfo accountInfo)
      {    
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
         contact.AccountId = account.AccountId;
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
            CompanyPhoneNumber = GetPhoneNumber(o.CompanyPhone),
            CompanyEmail = o.CompanyEmail,
            Address = o.Address,
            Description = o.Description,
            CreatedDate = o.CreatedDate,
            UpdatedDate = o.UpdatedDate,

            ContactId = o.ContactId,
            ContactName = o.ContactName,
            Department = o.Department,
            Position = o.Position,
            ContactPhoneNumber = GetPhoneNumber(o.PhoneNumber)
         }).AsEnumerable();

         return clientAccounts;
      }

      private static string[] GetPhoneNumber(string parameter)
      {
         var phoneNumber = parameter;
         var arrNum = new string[4];
         var a = string.Format("{0:###-####-####}", phoneNumber); // 11
       
         var c = string.Format("{0:##-###-####}", phoneNumber);
         var d = string.Format("{0:###-###-####}", phoneNumber);
         var b = string.Format("{0:##-####-####}", phoneNumber);

         switch (phoneNumber.Length)
         {
            case 11 :
               {
                  arrNum = string.Format("{0:###-####-####}", phoneNumber).Split('-');
                  break;
               }
            case 10:
               {
                  if(phoneNumber[1] == '2')
                  {                     
                     arrNum = string.Format("{0:##-####-####}", phoneNumber).Split('-');
                  }
                  else
                  {
                     arrNum = string.Format("{0:###-###-####}", phoneNumber).Split('-');
                  }
                  break;
               }
            case 9:
               {
                  arrNum = string.Format("{0:##-###-####}", phoneNumber).Split('-');
                  break;
               }
         }

         return arrNum;
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
