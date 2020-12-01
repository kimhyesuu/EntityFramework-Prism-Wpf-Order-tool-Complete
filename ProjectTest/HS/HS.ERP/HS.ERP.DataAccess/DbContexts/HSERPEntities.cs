namespace HS.ERP.DataAccess.DbContexts
{
   using HS.ERP.DataAccess.Domain;
   using System.Data.Entity;

   public class HSERPEntities : DbContext
   {
      public HSERPEntities(string connectString) : base(connectString) { }

      protected override void OnModelCreating(DbModelBuilder modelBuilder) { }

      public DbSet<DAccount> dAccountInfo { get; set; }

      public DbSet<DTelePhone> dTelePhone { get; set; }

      public DbSet<DOrder> dOrder { get; set; }

      public DbSet<DOrderProduct> dOrderProduct { get; set; }

      public DbSet<DProduct> dProduct { get; set; }

   }
}
