using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands {
    public class LogException : BaseCommand<Exception>, ICommand {

        public Exception Exception { get; set; }
        public Object AdditionalData { get; set; }

        public LogException(IHttpContext context) 
            : base(context) {
        }

        public void Execute() {

            base.AddException(this.Exception, AdditionalData);

        }

        public void Log(Exception ex, PolicyInfo policy) {
            base.LogException(ex, policy);
        }
    }
}
