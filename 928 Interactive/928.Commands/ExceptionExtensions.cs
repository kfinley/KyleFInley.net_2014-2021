using _928.Core.ExceptionHandling;
using _928.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands {
    public static class ExceptionExtensions {
        public static void LogException(PolicyInfo policy, CommandDispatcher dispatcher) {
            LogException(new Exception(), policy, dispatcher);
        }
        public static void LogException(this Exception ex, PolicyInfo policy, CommandDispatcher dispatcher) {

            var saveException = CommandFactory.Create<SaveException>();
            saveException.Data = new CoreException {
                ExceptionType = policy.OriginalException.ToString(),
                ExceptionMessage = policy.Message == null ? "" : policy.Message,
                AdditionalData = policy.Data == null ? null : JObject.FromObject(policy.Data).ToString()
            };

            dispatcher.Run(saveException, saveException.UnitOfWork.Commit);
        }
    }
}
