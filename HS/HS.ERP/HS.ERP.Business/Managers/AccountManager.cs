using HS.ERP.Business.Models;
using HS.ERP.Repository;

namespace HS.ERP.Business.Managers
{
   public class AccountManager 
   {
      readonly AccountRepogitory _repository;
   
      public AccountManager()   
       =>  this._repository = InfoList.GetCurrentAccounts;      
      
      // GetAll 성립시키자
      public bool Add(Account account)
      {
         if (_repository.Search(account.AccountId) is null)
         {
            _repository.Add(account);
            return true;
         }
         return false;
      }

      public bool Remove(long? id)
      {
         Account account = _repository.Search(id);

         if (account != null)
         {
            _repository.Remove(account);
            return true;
         }
         return false;
      }

      public Account Search(int id)
      {
         return _repository.Search(id);
      }
   }
}
