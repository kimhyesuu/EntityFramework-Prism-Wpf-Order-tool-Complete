using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HS.ERP.DataAccess.ERPDbContext;

namespace HS.ERP.DataAccess.Repogitory
{
   public class AccountRepogitory<TEntity> : IAccountRepogitory<TEntity> where TEntity : class
   {
      private ERPDemoEntities _context { get; }
      private DbSet<TEntity> _table { get; }

      public AccountRepogitory(ERPDemoEntities context)
      {
         this._context = context;
         _table = _context.Set<TEntity>();
      }

      public IEnumerable<TEntity> GetAll()
      {     
         return _table.ToList();
      }

      public TEntity GetById(object id)
      {
         return _table.Find(id);
      }

      public TEntity Insert(TEntity parameter)
      {
         var para = _table.Add(parameter);
         _context.SaveChanges();
         return para;
      }

      public void Update(TEntity parameter)
      {
         _table.Attach(parameter);
         _context.Entry(parameter).State = EntityState.Modified;
         _context.SaveChanges();
      }

      public void Delete(TEntity parameter)
      {
         if(_context.Entry(parameter).State == EntityState.Detached)
         {
            _table.Attach(parameter);
         }
         
         _table.Remove(parameter);
         _context.SaveChanges();
      }

      #region Ex1
      //public T GetById(int id)
      //{
      //   using (ERPDemoEntities db = new ERPDemoEntities())
      //   {
      //      return db.AccountInfo.Find();
      //   }
      //}

      //public AccountInfo Insert(AccountInfo Accountparameter)
      //{
      //   using (ERPDemoEntities db = new ERPDemoEntities())
      //   {
      //      db.AccountInfo.Add(Accountparameter);
      //      db.SaveChanges();
      //      return Accountparameter;
      //   }
      //}

      //public void Update(AccountInfo Accountparameter)
      //{
      //   using (ERPDemoEntities db = new ERPDemoEntities())
      //   {
      //      db.AccountInfo.Attach(Accountparameter);
      //      db.Entry(Accountparameter).State = System.Data.Entity.EntityState.Modified;
      //      db.SaveChanges();
      //   }
      //}

      //public void Delete(AccountInfo Accountparameter)
      //{
      //   using (ERPDemoEntities db = new ERPDemoEntities())
      //   {
      //      db.AccountInfo.Attach(Accountparameter);
      //      db.AccountInfo.Remove(Accountparameter);
      //      db.SaveChanges();
      //   }
      //}
      #endregion 
   }
}


