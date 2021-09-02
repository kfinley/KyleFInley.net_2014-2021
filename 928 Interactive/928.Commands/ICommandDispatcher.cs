using _928.Core.Interfaces;
using System;
using System.Threading.Tasks;
namespace _928.Commands {
    public interface ICommandDispatcher {
        ICommandDispatcher Run(ICommand command);
        ICommandDispatcher Run(ICommand command, Action callback, bool runInBackground = false);
        ICommandDispatcher Run(ICommand command, bool async);
        ICommandDispatcher Run(ICommand command, bool async, Action callback, bool runInBackground = false);

        Task GetTask(int id);

        IHttpContext HttpContext { get; set; }
    }
}
