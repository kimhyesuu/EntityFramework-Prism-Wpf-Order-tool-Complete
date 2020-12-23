using System.Collections.Generic;
using System.Linq;
using HS.ERP.Business.Converter;
using HS.ERP.Business.Models;
using HS.ERP.DataAccess.Domain;
using HS.ERP.DataAccess.UnitOfWorks;

namespace HS.ERP.Business.Services
{
   public class AccountService : IDataService<Account>
   {
      IERPUnitOfWork unitOfWork { get; }

      public AccountService()    
        => unitOfWork = new ERPUnitOfWork(DBConnect.ConnectString);

      public IEnumerable<Account> GetAll()
         => ConvertToModel.DomainToClient(unitOfWork.Accounts.GetAll());

      public void SendEntityStatus(IEnumerable<Account> accounts)
      {
         if (accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted).FirstOrDefault() != null)
         {
            var list = new List<DAccount>();

            foreach (var info in accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Inserted))
            {
               list.Add(ConvertToModel.ClientToDomain(info));
              
            }

            unitOfWork.Accounts.Insert(list);
         }

         if (accounts.Where(deleteInfo => deleteInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted).FirstOrDefault() != null)
         {
            var list = new List<long?>();

            foreach (var info in accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Deleted))
            {
               list.Add(info.AccountId);
            }

            unitOfWork.Accounts.Delete(list);
         }

         if (accounts.Where(updateInfo => updateInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Updated).FirstOrDefault() != null)
         {
            var list = new List<DAccount>();

            foreach (var info in accounts.Where(addInfo => addInfo.EntityState == HS.ERP.Business.Models.Enums.EntityStateOption.Updated))
            {
               list.Add(ConvertToModel.ClientToDomain(info));
            }

            unitOfWork.Accounts.Update(list);

         }
      }
   }
}
