using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Configuration;
using System.Linq.Expressions;

namespace _928.Core.Data.Repository
{
    /// <summary>
    /// Repository base class used with DbContext
    /// </summary>
    /// <typeparam name="TContext">Type of DdContext that this repository operates on</typeparam>
    public class EFRepository<TContext> : IDisposable, IRepository<TContext>
        where TContext : DbContext, IObjectContextAdapter, new()
    {
        private TContext context;

        /// <summary>
        /// Create new instance of repository
        /// </summary>
        /// <param name="connectionStringName">Connection string name from .config file</param>
        public EFRepository(string connectionStringName)
        {
            context = new TContext();
            context.Database.Connection.ConnectionString =
                ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public EFRepository()
        {
            context = new TContext();
            context.Database.Connection.ConnectionString =
                ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        }

        /// <summary>
        /// Dispose repository
        /// </summary>
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }


        /// <summary>
        /// Select data from database
        /// </summary>
        /// <typeparam name="TItem">Type of data to select</typeparam>
        /// <returns></returns>
        public IEnumerable<TItem> Select<TItem>()
           where TItem : class, new()
        {
            DbSet<TItem> set = context.Set<TItem>();
            foreach (var item in set)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Select data from database using a where clause
        /// </summary>
        /// <typeparam name="TItem">Type of data to select</typeparam>
        /// <param name="whereClause">Where clause / function</param>
        /// <returns></returns>
        public IEnumerable<TItem> Select<TItem>(Expression<Func<TItem, bool>> whereClause)
           where TItem : class, new()
        {
            IQueryable<TItem> data = context.Set<TItem>().Where(whereClause);
            foreach (var item in data)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Select data from database using a where clause
        /// </summary>
        /// <typeparam name="TItem">Type of data to select</typeparam>
        /// <param name="whereClause">Where clause / function</param>
        /// <param name="orderBy">Order by clause</param>
        /// <returns></returns>
        public IEnumerable<TItem> Select<TItem>(
          Expression<Func<TItem, bool>> whereClause,
          Expression<Func<TItem, object>> orderBy)
           where TItem : class, new()
        {
            IOrderedQueryable<TItem> data = context.Set<TItem>().Where(whereClause).OrderBy(orderBy);
            foreach (var item in data)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Select data from database using a where clause
        /// </summary>
        /// <typeparam name="TItem">Type of data to select</typeparam>
        /// <param name="whereClause">Where clause / function</param>
        /// <param name="orderBy">Order by clause</param>
        /// <returns></returns>
        public IEnumerable<TItem> Select<TItem>(
          Expression<Func<TItem, bool>> whereClause,
          Expression<Func<TItem, object>> orderBy,
          int skip = 0,
          int top = 0)
           where TItem : class, new()
        {
            IQueryable<TItem> data = context.Set<TItem>().Where(whereClause);
            if (skip > 0)
            {
                data = data.Skip(skip);
            }
            if (top > 0)
            {
                data = data.Take(top);
            }
            data = data.OrderBy(orderBy);
            foreach (var item in data)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Insert new item into database
        /// </summary>
        /// <typeparam name="TItem">Type of item to insert</typeparam>
        /// <param name="item">Item to insert</param>
        /// <returns>Inserted item</returns>
        public TItem Insert<TItem>(TItem item)
            where TItem : class, new()
        {
            DbSet<TItem> set = context.Set<TItem>();
            set.Add(item);
            context.SaveChanges();
            return item;
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <typeparam name="TItem">Type of item to update</typeparam>
        /// <param name="item">Item to update</param>
        /// <returns>Updated item</returns>
        public TItem Update<TItem>(TItem item)
            where TItem : class, new()
        {
            DbSet<TItem> set = context.Set<TItem>();
            set.Attach(item);
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
            return item;
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <typeparam name="TItem">Type of item to delete</typeparam>
        /// <param name="item">Item to delete</param>
        public void Delete<TItem>(TItem item)
           where TItem : class, new()
        {
            DbSet<TItem> set = context.Set<TItem>();
            var entry = context.Entry(item);
            if (entry != null)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                set.Attach(item);
            }
            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
        }

    }
}
