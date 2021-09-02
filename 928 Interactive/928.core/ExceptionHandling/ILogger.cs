using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.ExceptionHandling {
    public interface ILogger {
        void LogException(PolicyInfo policy);
    }
}
