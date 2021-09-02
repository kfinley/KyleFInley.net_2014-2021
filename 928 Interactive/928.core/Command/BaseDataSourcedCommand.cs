using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _928.Core.Data.Repository;
using _928.Core.Data;

namespace _928.Core.Command {

    public abstract class BaseDataSourcedCommand<TData, TEntity> : BaseCommand<TData> where TEntity : EntityBase {
        public BaseDataSourcedCommand(IRepository<TEntity> repository) {
            this.repository = repository;
            this.UnitOfWork = repository.UnitOfWork;
        }

        protected IRepository<TEntity> repository;

        public Guid Id { get; set; }

        public void SharedContext<T>(IRepository<T> repository) where T : EntityBase {
            this.repository.Context = repository.Context;
        }

        public IUnitOfWork UnitOfWork { get; set; }
    }

    public abstract class BaseDataSourcedCommand<T> : BaseCommand<T> where T : EntityBase {
        public BaseDataSourcedCommand(IRepository<T> repository) {
            this.repository = repository;
            this.UnitOfWork = repository.UnitOfWork;
        }
        
        protected IRepository<T> repository;

        public Guid Id { get; set; }

        public void SharedContext<T>(IRepository<T> repository) where T : EntityBase {
            this.repository.Context = repository.Context;
        }

        public IUnitOfWork UnitOfWork { get; set; }

    }
}
