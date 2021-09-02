using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using _928.Entities;
using _928.Core.Interfaces.Data;

namespace _928.Data.Repository
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : EntityBase
    {

        TItem Insert<TItem>(TItem item) where TItem : EntityBase;

        TItem Update<TItem>(TItem item) where TItem : EntityBase;

        void Delete<TItem>(TItem item) where TItem : EntityBase;

        IEnumerable<TItem> Select<TItem>() where TItem : EntityBase;

        IEnumerable<TItem> Select<TItem>(Expression<Func<TItem, bool>> whereClause) where TItem : EntityBase;

        IEnumerable<TItem> Select<TItem>(Expression<Func<TItem, bool>> whereClause, Expression<Func<TItem, object>> orderBy)
          where TItem : EntityBase;

        IQueryable<TEntity> All();

        IQueryable<TEntity> Local(Func<TEntity, bool> predicate);

        IUnitOfWork UnitOfWork { get; set; }

        IDataContext Context { get; set; }

        void Dispose();
        
    }

    //public interface IRepository<TEntity> where TEntity : EntityBase
    //{
    //    bool Insert(TEntity entity);
    //    bool Update(TEntity entity);
    //    bool Delete(TEntity entity);
    //    IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
    //    IList<TEntity> GetAll();
    //    TEntity GetById(Guid id);
    //}
}
