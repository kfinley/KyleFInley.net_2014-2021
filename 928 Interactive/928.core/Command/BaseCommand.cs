using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Command
{
    public abstract class BaseCommand {
        protected CommandDispatcher dispatcher = new CommandDispatcher();
        
        public bool IsAsync { get; set; }
        public Task Task { get; set; }

    }

    public abstract class BaseCommand<T> : BaseCommand
    {
        protected T data;

        public T Data
        {
            get
            {
                if (IsAsync && !Task.IsCompleted)
                    Task.WaitAll(new Task[] { this.Task });
                return data;
            }
            set { data = value; }
        }
    }
}
