using System.Collections.Generic;
using HS.ERP.Business.Converter;
using HS.ERP.Business.Models;
using HS.ERP.DataAccess.Domain;
using HS.ERP.DataAccess.UnitOfWorks;

namespace HS.ERP.Business.Services
{
   public class AccountService : IServiceLogic<Account>
   {
      IERPUnitOfWork unitOfWork { get; }

      public AccountService()
      {
         unitOfWork = new ERPUnitOfWork(DBConnect.ConnectString);
      }

      public void Delete(object id)
      {
         var accountId = id;

         unitOfWork.Accounts.Delete(accountId);
         unitOfWork.AccountContacts.Delete(accountId);
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

         ConvertToModel.ConvertToAccountInfoDomain(account, accountInfo);
         ConvertToModel.ConvertToContactDomain(account, contact);

         unitOfWork.Accounts.Insert(accountInfo);
         unitOfWork.AccountContacts.Insert(contact);

         return account;
      }

      public void Update(Account parameter)
      {
         throw new System.NotImplementedException();
      }
   }
}
