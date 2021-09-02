using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _928.Core {
    public class CommandDispatcher {
        private List<Exception> exceptions = new List<Exception>();

        public List<Exception> Exceptions {
            get {
                return exceptions;
            }
        }
        public CommandDispatcher Run(ICommand command, Action callback) {
            this.Run(command, true, callback);
            return this;
        }

        public CommandDispatcher Run(ICommand command) {
            this.Run(command, true, null);
            return this;
        }

        public CommandDispatcher Run(ICommand command, bool async) {
            this.Run(command, async, null);
            return this;
        }

        public CommandDispatcher Run(ICommand command, bool async, Action callback) {
            command.IsAsync = async;

            if (command.IsAsync) {
                var task = Task.Factory.StartNew(() => {
                    try {
                        command.Execute();
                        if (callback != null) {
                            callback();
                        }
                    } catch (Exception ex) {
                        this.exceptions.Add(ex);
                        throw ex;
                    }
                });
                //}, TaskCreationOptions.AttachedToParent);

                command.Task = task;

                AddTaskToApplicaitonState(task);


            } else {
                try {
                    command.Execute();
                    if (callback != null) {
                        callback();
                    }
                } catch (Exception ex) {
                    this.exceptions.Add(ex);
                    throw ex;
                }
            }
            return this;
        }

        public void AddTaskToApplicaitonState(Task task) {
            if (HttpContext.Current.Application["tasks"] == null) {
                HttpContext.Current.Application["tasks"] = new ConcurrentBag<Task>();
            }

            ((ConcurrentBag<Task>)HttpContext.Current.Application["tasks"]).Add(task);
        }

        public Task GetTask(int id) {
            if (HttpContext.Current.Application["tasks"] != null) {
                return ((ConcurrentBag<Task>)HttpContext.Current.Application["tasks"]).Where(t => t.Id == id).FirstOrDefault();
            }
            return null;
        }
    }
}
