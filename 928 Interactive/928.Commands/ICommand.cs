using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands
{
    public interface IQueryableCommand {
        IQueryable<IEntity> Query();
        void SharedContext<T>(IRepository<T> repository) where T : EntityBase;
    }

    public interface IDataCommand<T> : ICommand {
        T Data { get; set; }
        Guid Id { get; set; }
    }

    public interface ICommand
    {
        void Execute();

        Task Task { get; set; }
        bool IsAsync { get; set; }
        IHttpContext HttpContext { get; set; }

        void AddException(Exception ex, object additionalData = null);

        bool HasErrors { get; }
    }
}
