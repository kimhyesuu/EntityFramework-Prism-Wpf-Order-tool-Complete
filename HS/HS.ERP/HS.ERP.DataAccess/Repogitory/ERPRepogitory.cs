namespace HS.ERP.DataAccess.Repogitory
{
   using HS.ERP.DataAccess.DbContexts;
   using System;
   using System.Collections.Generic;
   using System.Data.Entity;
   using System.Data.Entity.Infrastructure;
   using System.Diagnostics;
   using System.Linq;

   public class ERPRepogitary<TEntity> : IERPRepogitary<TEntity> where TEntity : class
   {
      private HSERPEntities _context;

      public ERPRepogitary(HSERPEntities context)
      {
         this._context = context;
      }

      public IEnumerable<TEntity> GetAll()
      {
         try
         {
            var list = _context.Set<TEntity>().Select(a => a).ToList();
            return list;
         }
         catch (Exception e)
         {
            Console.WriteLine(e.InnerException.Message);
            return null;
         }
      }

      public TEntity GetById(object id)
      {
         try
         {
            return _context.Set<TEntity>().Find(id);            
         }
         catch
         {
            return null;
         }
       
      }

      public TEntity Insert(TEntity parameter)
      {
         try
         {
            var para = _context.Set<TEntity>().Add(parameter);
            return para;

         }
         catch (DbUpdateException e)
         {
            Console.WriteLine(e.InnerException.Message);
            return null;
         }
      }

      public void Update(TEntity parameter)
      {
         try
         {
            _context.Set<TEntity>().Attach(parameter);
            _context.Entry(parameter).State = EntityState.Modified;
         }
         catch(Exception e)
         {
            Debug.WriteLine(e.Message);
         }       
      }

      public void Delete(TEntity parameter)
      {
         if (_context.Entry(parameter).State == EntityState.Detached)
         {
            _context.Set<TEntity>().Attach(parameter);
         }
         _context.Set<TEntity>().Remove(parameter);
      }
   }
}
