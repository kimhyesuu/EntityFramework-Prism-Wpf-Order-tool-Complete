namespace HS.ERP.Business.Managers
{
   using HS.ERP.Repository;

   public sealed class InfoList
   {
      private static AccountRepogitory _accountIntances = null;
      public static AccountRepogitory GetCurrentAccounts
      {
         get
         {
            if (_accountIntances is null)
               _accountIntances = new AccountRepogitory();
            return _accountIntances;
         }
      }

      private static ProductRepogitory _ProductIntances = null;
      public static ProductRepogitory GetCurrentProducts
      {
         get
         {
            if (_ProductIntances is null)
               _ProductIntances = new ProductRepogitory();
            return _ProductIntances;
         }
      }
   }
}
