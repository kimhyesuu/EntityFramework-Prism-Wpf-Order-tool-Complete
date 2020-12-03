using HS.ERP.Business.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HS.ERP.Repository
{
   public class AccountRepogitory
   {
      private static List<Account> _accounts;

      public AccountRepogitory()
        => _accounts = new List<Account>();

      public ObservableCollection<Account> GetAll()     
        => new ObservableCollection<Account>(_accounts);

      public void Add(Account account)
        => _accounts.Add(account);

      public void Remove(Account account)
        => _accounts.Remove(account);

      public void Update(Account account)
      {
         var result = Search(account);

         if(result != null)
         {
            Remove(result);
            Add(account);
         }       
      }

      public Account Search(Account account)
      {
         var index = GetIndex(account);
         return index > -1 ? _accounts[index] : null;             
      }

      public int GetIndex(Account account)
      {
         var index = -1;
         if (_accounts.Count() > 0)
            index = _accounts.IndexOf(account);

         return index;       
      }
   }
}
