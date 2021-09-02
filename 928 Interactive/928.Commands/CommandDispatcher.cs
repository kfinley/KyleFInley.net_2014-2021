using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _928.Commands {
    public class CommandDispatcher : ICommandDispatcher  {

        private IHttpContext context;

        public CommandDispatcher(IHttpContext context) {
            this.context = context;
        }

        public ICommandDispatcher Run(ICommand command, Action callback, bool runInBackground = false) {
            this.Run(command, true, callback, runInBackground);
            return this;
        }

        public ICommandDispatcher Run(ICommand command) {
            this.Run(command, true, null);
            return this;
        }

        public ICommandDispatcher Run(ICommand command, bool async) {
            this.Run(command, async, null);
            return this;
        }

        public ICommandDispatcher Run(ICommand command, bool async, Action callback, bool runInBackground = false) {
            command.IsAsync = async;

            if (command.IsAsync) {

                var task = Task.Factory.StartNew(() => {

                    try {
                        command.HttpContext = this.context;
                        command.Execute();
                        if (callback != null) {
                            callback();
                        }
                    } catch (Exception ex) {
                        command.AddException(ex);
                    }
                });

                command.Task = task;

                if (runInBackground) {
                    AddTaskToApplicaitonState(task);
                }
            } else {
                try {
                    command.Execute();
                    if (callback != null) {
                        callback();
                    }
                } catch (Exception ex) {
                    command.AddException(ex);
                }
            }
            return this;
        }

        //private static CacheItemRemovedCallback OnCacheRemove = null;

        //private void AddBackgroundTask(string name) {

        //    OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);
        //    HttpRuntime.Cache.Insert(name, seconds, null,
        //        DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration,
        //        CacheItemPriority.NotRemovable, OnCacheRemove);
        //}

        //public void CacheItemRemoved(string k, object v, CacheItemRemovedReason r) {
        //    // do stuff here if it matches our taskname, like WebRequest
        //    // re-add our task so it recurs
        //    AddTask(k, Convert.ToInt32(v));
        //}

        public void AddTaskToApplicaitonState(Task task) {

            try {
                if (context.AppContext["tasks"] == null) {
                    context.AppContext["tasks"] = new ConcurrentBag<Task>();
                }

                ((ConcurrentBag<Task>)context.AppContext["tasks"]).Add(task);
            } catch (Exception ex) {
                var foo = ex;
            }
        }

        public Task GetTask(int id) {
            if (context.AppContext["tasks"] != null) {
                return ((ConcurrentBag<Task>)context.AppContext["tasks"]).Where(t => t.Id == id).FirstOrDefault();
            }
            return null;
        }

        public IHttpContext HttpContext {
            get {
                return this.context;
            }
            set {
                this.context = value;
            }
        }
    }
}
