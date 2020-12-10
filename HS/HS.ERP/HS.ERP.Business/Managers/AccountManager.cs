using HS.ERP.Business.Models;
using HS.ERP.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HS.ERP.Business.Managers
{
   public class AccountManager : IRepogitoryManager<Account>
   {
      readonly AccountRepogitory _repository;
   
      public AccountManager()   
       =>  this._repository = InfoList.GetCurrentAccounts;      
            
      public ObservableCollection<Account> GetAll()
      {
         var result = _repository.GetAll();

         if(result.FirstOrDefault() != null)
         {
            return result;
         }
         return null;
      }

      public void AddRange(IEnumerable<Account> list)
      {
         foreach(var accountInfo in list)
         {
            _repository.Add(accountInfo);
         }
      }

      public bool Add(Account account)
      {
         if (_repository.Search(account) is null)
         {
            _repository.Add(account);
            return true;
         }
         return false;
      }

      public bool Remove(Account account)
      {
         if (_repository.Search(account) != null)
         {
            _repository.Remove(account);
            return true;
         }
         return false;
      }

      public bool Update(Account account)
      {
         if(_repository.Search(account) is null)
         {
            _repository.Update(account);
            return true;
         }

         return false;
      }

      public Account Search(Account account)
      {
         return _repository.Search(account);
      }
   }
}
