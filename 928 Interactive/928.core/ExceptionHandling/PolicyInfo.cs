using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.ExceptionHandling {
    public class PolicyInfo {
        public ExceptionPolicyBehavior Behavior { get; set; }
        public string Message { get; set; }
        public Type NewException { get; set; }
        public Type OriginalException { get; set; }
        public string Source { get; set; }
        public object Data { get; set; }
    }

    public enum ExceptionPolicyBehavior {
        Wrap = 1,
        Replace,
        ReThrow,
        Suppress
    }
}
