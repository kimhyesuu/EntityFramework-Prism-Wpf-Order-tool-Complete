namespace HS.ERP.Repository
{
   using System.Collections.Generic;

   public sealed class InfoList<TEntity>  
   {
      private static List<TEntity> _accounts = null;

      public static List<TEntity> GetCurrentAccounts
      {
         get
         {
            if (_accounts is null)
               _accounts = new List<TEntity>();
            return _accounts;
         }
      }
   }
}
