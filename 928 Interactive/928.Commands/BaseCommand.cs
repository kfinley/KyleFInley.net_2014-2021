using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Entities;
using _928.Core;

using TypeMerger;

namespace _928.Commands {
    public abstract class BaseCommand : IDisposable {
        protected ICommandDispatcher dispatcher;
        private List<Exception> exceptions;
        protected IHttpContext context;

        public BaseCommand(IHttpContext context) {
            this.context = context;
            this.dispatcher = new CommandDispatcher(context);
        }

        public bool IsAsync { get; set; }
        public Task Task { get; set; }
        public IHttpContext HttpContext {
            get {
                return this.context;
            }
            set {
                this.context = value;
            }
        }

        public void AddException(Exception ex, Object additionalData = null) {

            var handler = new ExceptionHandler(new ExceptionHandlingPolicy(
                new PolicyInfo {
                    OriginalException = ex.GetType(),
                    Behavior = ExceptionPolicyBehavior.Suppress,
                    NewException = typeof(Exception),
                    Message = ex.Message,
                    Source = ex.Source,
                    Data = additionalData == null ? new { StackTrace = ex.StackTrace } : Merger.Merge(additionalData, new { StackTrace = ex.StackTrace })
                }));

            handler.HandleException(ex, this.LogException);

            if (this.exceptions == null)
                this.exceptions = new List<Exception>();

            this.exceptions.Add(ex);
        }

        public List<Exception> Exceptions {
            get {
                return exceptions;
            }
        }
        public bool HasErrors {
            get {
                if (IsAsync && !Task.IsCompleted)
                    Task.Wait();

                if (this.exceptions != null && this.exceptions.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        protected virtual void LogException(Exception ex, PolicyInfo policy) {

            //if (ex.AnySource("EntityFramework") == false) { 
            //if (policy.Source != "EntityFramework") {
                var saveException = CommandFactory.Create<SaveException>();
                saveException.Data = new CoreException {
                    ExceptionType = policy.OriginalException.ToString(),
                    ExceptionMessage = policy.Message == null ? "" : policy.Message,
                    AdditionalData = policy.Data == null ? null : JObject.FromObject(policy.Data).ToString()
                };

                dispatcher.Run(saveException, false, saveException.UnitOfWork.Commit);
            //}
        }

        public void Dispose() {
            if (this.Task.IsCompleted != true) {
                GC.SuppressFinalize(this);
            }
        }
    }

    public abstract class BaseCommand<T> : BaseCommand {

        protected T data;

        public BaseCommand(IHttpContext context)
            : base(context) {
        }

        public T Data {
            get {
                if (IsAsync && !Task.IsCompleted)
                    Task.Wait();
                if (this.HasErrors) {
                        
                    var message = this.Exceptions.Select(e => e.GetAllMessages()).Aggregate((current, next) => current + Environment.NewLine + next + Environment.NewLine).ToString();
                    throw new Exception("Command failed with {0} errors! {1} Exceptions: {2}".FormatWith(this.Exceptions.Count, Environment.NewLine, message));
                }
                return data;
            }
            set { data = value; }
        }

    }
}
