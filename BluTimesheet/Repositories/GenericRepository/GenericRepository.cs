using BluTimesheet.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace BluTimesheet.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected TimesheetDbContext context;
        protected DbSet<T> dbSet;

        public GenericRepository(TimesheetDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
                
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T Get(object id)
        {
            return dbSet.Find(id);
        }
        public T Add(T obj)
        {
            
            dbSet.Add(obj);
            Save();
            return obj;
        }
        public void Remove(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        private void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public T Update(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
            return obj;
        }
        public void Save()
        {
            try
            {
                context.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    }
                }
            }
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> filter)
        {            
            return dbSet.Where(filter).ToList();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}