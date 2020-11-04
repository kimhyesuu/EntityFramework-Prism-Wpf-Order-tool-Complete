using HS.ERP.Business.Models;
using HS.ERP.Business.UnitOfWorks;
using System.Collections.Generic;

namespace HS.ERP.Business.BLL
{
   public class AccountService : IDataService<AccountInfo>
   {
      IUnitOfWork unitOfWork { get; } 

      public AccountService()
      {
         unitOfWork = new UnitOfWork();
      }

      public IEnumerable<AccountInfo> GetAll()
      {
         return unitOfWork.Accounts.GetAll();
      }

      public AccountInfo Insert(AccountInfo parameter)
      {
         var AddAccount = unitOfWork.Accounts.Insert(parameter);
        
         return AddAccount;
      }

      public void Update(AccountInfo parameter)
      {
         unitOfWork.Accounts.Update(parameter);       
      }

      public void Delete(AccountInfo parameter)
      {
         unitOfWork.Accounts.Delete(parameter);
      }
   }
}
