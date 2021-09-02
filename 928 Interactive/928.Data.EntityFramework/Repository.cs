
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

            item.Id = Guid.NewGuid();
            return context.Set<TItem>().Add(item);
        }

        public TItem Update<TItem>(TItem item) where TItem : EntityBase {

            context.Set<TItem>().Attach(item);
            context.Entry(item).State = EntityState.Modified;

            return item;
        }

        public void Delete<TItem>(TItem item) where TItem : EntityBase {

        }

        public IEnumerable<TItem> Select<TItem>() where TItem : EntityBase {
            return new List<TItem>();
        }

        public IEnumerable<TItem> Select<TItem>(Expression<Func<TItem, bool>> whereClause) where TItem : EntityBase {
            return new List<TItem>();
        }

        public IEnumerable<TItem> Select<TItem>(Expression<Func<TItem, bool>> whereClause, Expression<Func<TItem, object>> orderBy)
          where TItem : EntityBase {
            return new List<TItem>();
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
