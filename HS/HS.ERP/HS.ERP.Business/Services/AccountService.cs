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

       
      }

      //public IEnumerable<Account> GetAll()
      //{
      //   //이너 조인 찾아볼것
      //   return 
      //}

      public Account Insert(Account parameter)
      {
         var account = parameter;
        

         return account;
      }

      public void Update(Account parameter)
      {
         throw new System.NotImplementedException();
      }
   }
}
