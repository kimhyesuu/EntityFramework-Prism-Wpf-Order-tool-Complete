using HS.ERP.Business.Models;
using System.Collections.Generic;

namespace HS.ERP.Repository
{
   public class AccountRepogitory
   {
      private static List<Account> _accounts;
      /// <summary>
      /// 1. 값을 로딩할 때 받고 
      /// </summary>
      public AccountRepogitory()
       => _accounts = new List<Account>();
      
      public void Add(Account account)
        => _accounts.Add(account);

      public void Remove(Account account)
        => account.EntityState = Business.Models.Enums.EntityStateOption.Deleted;

      // 업데이트
      public void Update(Account account)
      {
         //값이 다른값이 있는가를 확인하는 절차가 필요
      }

      public Account Search(long? id)
      {
         int index = GetIndex(id);
         return index > -1 ? _accounts[index] : null;  
      }

      public int GetIndex(long? id)
      {
         int index = -1;
         if (_accounts.Count > 0)
         {
            for (int i = 0; i < _accounts.Count; i++)
            {
               if (_accounts[i].AccountId == id)
               {
                  index = i;
                  break;
               }
            }
         }
         return index;
      }

      // 실제로 딜리트되어있는 부부을 지운다.
      // 업데이트도 한다.

      //저장했을시 DB업데이트
   }
}
