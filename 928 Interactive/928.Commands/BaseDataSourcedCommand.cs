using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _928.Entities;
using _928.Data.Repository;
using _928.Data;
using _928.Core.Interfaces.Data;
using _928.Core.Interfaces;
using _928.Core;
using System.Linq.Expressions;

namespace _928.Commands
{

    public abstract class BaseCachedCommand<T> : BaseCommand<T> where T : class
    {

        public BaseCachedCommand(IHttpContext context)
            : base(context)
        {

        }
        private string cacheKey;

        protected void SaveToCache(T item, params string[] itemKeys)
        {

            var cachedItems = context.Cache[cacheKey] as Dictionary<string, T>;

            if (item.IsNotNull())
            {
                foreach (var key in itemKeys)
                {
                    cachedItems[key] = item;
                }
                context.Cache[cacheKey] = cachedItems;
            }
        }

        protected T RetrieveFromCache(string itemKey, Func<T> exp)
        {
            return this.RetrieveFromCache(itemKey, exp, x => (new string[] { itemKey }));
        }

        protected T RetrieveFromCache(string itemKey, Func<T> exp, Func<T, string[]> itemKeys)
        {

            var item = this.RetrieveFromCache(itemKey);

            if (item.IsNull())
            {
                item = exp.Invoke();
                if (item.IsNotNull())
                {
                    this.SaveToCache(item, itemKeys.Invoke(item));
                }
            }

            return item;
        }

        protected T RetrieveFromCache(string itemKey)
        {

            var cachedItems = context.Cache[cacheKey] as Dictionary<string, T>;

            if (cachedItems.IsNull())
            {
                cachedItems = new Dictionary<string, T>();
                context.Cache[cacheKey] = cachedItems;
            }

            return cachedItems.ContainsKey(itemKey) ? cachedItems[itemKey] : null;

        }

        protected string CacheKey
        {
            get { return this.cacheKey; }
            set { this.cacheKey = value; }
        }

    }

    public abstract class BaseDataSourcedCommand<TData, TEntity> : BaseCachedCommand<TData>
        where TEntity : EntityBase
        where TData : class
    {


        public BaseDataSourcedCommand(IRepository<TEntity> repository, IHttpContext context)
            : base(context)
        {
            this.repository = repository;
            this.UnitOfWork = repository.UnitOfWork;
        }

        protected IRepository<TEntity> repository;

        public Guid Id { get; set; }

        public void SharedContext<T>(IRepository<T> repository) where T : EntityBase
        {
            this.repository.Context = repository.Context;
        }

        public IUnitOfWork UnitOfWork { get; set; }
    }

    public abstract class BaseDataSourcedCommand<T> : BaseCachedCommand<T>, IDisposable where T : EntityBase
    {
        protected IRepository<T> repository;
        private IList<EntityAssociation> associations;

        public BaseDataSourcedCommand(IRepository<T> repository, IHttpContext context)
            : base(context)
        {
            this.repository = repository;
            this.UnitOfWork = repository.UnitOfWork;

        }

        public Guid Id { get; set; }

        public void SharedContext<E>(IRepository<E> repository) where E : EntityBase
        {
            this.repository.Context = repository.Context;
        }

        public IUnitOfWork UnitOfWork { get; set; }

        public void AddAssociation(EntityAssociation association)
        {
            if (this.associations == null)
                this.associations = new List<EntityAssociation>();

            associations.Add(association);
        }

        public void SaveEntityAssociation(Guid entityid)
        {
            this.AddAssociation(new EntityAssociation
            {
                EntityId = entityid,
                Active = true
            });

            this.SaveEntityAssociations();

        }

        public void SaveEntityAssociations()
        {
            if (this.associations != null)
            {
                foreach (var association in this.associations)
                {

                    var saveAssociation = CommandFactory.Create<SaveEntityAssociation>();
                    association.Id = this.data.Id;
                    saveAssociation.Data = association;

                    saveAssociation.SharedContext(this.repository);
                    dispatcher.Run(saveAssociation, false);
                }
            }
        }

        public new void Dispose()
        {
            if (UnitOfWork.Committed == false)
                UnitOfWork.Commit();
        }
    }
}
