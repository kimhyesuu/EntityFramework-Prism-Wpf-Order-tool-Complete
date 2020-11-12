using System;
using System.Collections.Generic;
using HS.ERP.Business.Converter;
using HS.ERP.Business.Models;
using HS.ERP.DataAccess.Domain;
using HS.ERP.DataAccess.UnitOfWorks;

namespace HS.ERP.Business.Services
{
   public class AccountService : IServiceLogic<Account>
   {
      IERPUnitOfWork unitOfWork { get;} 

      public AccountService()
      {
         unitOfWork = new ERPUnitOfWork();
      }

      public void Delete(Account parameter)
      {
         throw new System.NotImplementedException();
      }

      public IEnumerable<Account> GetAll()
      {
         return ConvertToModel.ConvertToClient(unitOfWork.Accounts.GetAll(), unitOfWork.AccountContacts.GetAll());
      }
     
      public Account Insert(Account parameter)
      {         
         var account = parameter;
         var accountInfo = new DAccountInfo();
         var contact = new DContact();

         ConvertToModel.ConvertToAccountInfoDomain(parameter, accountInfo);
         ConvertToModel.ConvertToContactDomain(parameter, contact);

         unitOfWork.Accounts.Insert(accountInfo);
         unitOfWork.AccountContacts.Insert(contact);

         unitOfWork.Save();

         return account; 
      }

      public void Update(Account parameter)
      {
         throw new System.NotImplementedException();
      }
   }
}
