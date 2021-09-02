
using _928.Core.Interfaces.Data;
using _928.Data.Repository;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace _928.Data.EntityFramework {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase {
        private IDbContext context;
        private IUnitOfWork unitOfWork;
        
        public Repository(IDbContext context, IUnitOfWork unitOfWork) {
            this.context = context;
            this.unitOfWork = unitOfWork;

#if DEBUG
            ((DbContext)context).Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
        }

        public TItem Insert<TItem>(TItem item) where TItem : EntityBase {

                if (item.Id == Guid.Empty)
                    item.Id = Guid.NewGuid();

                item.ValidateData();
                return context.Set<TItem>().Add(item);
        }

        public TItem Update<TItem>(TItem item) where TItem : EntityBase {

                item.ValidateData();
                context.Set<TItem>().Attach(item);
                context.Entry(item).State = EntityState.Modified;

                return item;
        }

        public void Delete<TItem>(TItem item) where TItem : EntityBase {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> Select<TItem>() where TItem : EntityBase {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> Select<TItem>(Expression<Func<TItem, bool>> whereClause) where TItem : EntityBase {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> Select<TItem>(Expression<Func<TItem, bool>> whereClause, Expression<Func<TItem, object>> orderBy)
          where TItem : EntityBase {
              throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> All() {
            return context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Local(Func<TEntity, bool> predicate) {
            return context.Set<TEntity>().Local.Where(predicate).AsQueryable();
        }

        public IUnitOfWork UnitOfWork {
            get {
                return this.unitOfWork;
            }
            set {
                this.unitOfWork = value;
            }
        }

        public IDataContext Context {
            get {
                return this.context as IDataContext;
            }
            set {
                this.context = (IDbContext)value;
            }
        }

        public void Dispose() {
            this.context.Dispose();
        }
    }
}
