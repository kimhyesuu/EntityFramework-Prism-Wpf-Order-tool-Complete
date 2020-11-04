using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HS.ERP.DataAccess.ERPDbContext
{
   public class ERPDemoEntities : DbContext
   {
      public ERPDemoEntities()
        : base("name=ERPDemoEntities")
      {
      }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
         throw new UnintentionalCodeFirstException();
      } 
   }
}
