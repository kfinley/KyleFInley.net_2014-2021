using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.ExceptionHandling {
    public class ExceptionHandler {
        private ExceptionHandlingPolicy policy;

        public ExceptionHandler(ExceptionHandlingPolicy policy) {
            this.policy = policy;
        }

        public void HandleException(Exception ex, Action<Exception, PolicyInfo> log) {

            var policyInfo = policy.PolicyInfoFor(ex.GetType());

            if (log != null) {
                log(ex, policyInfo);
            }

            switch (policyInfo.Behavior) {
                case ExceptionPolicyBehavior.Wrap:
                    var wrappedException = (Exception)Activator.CreateInstance(policyInfo.NewException, policyInfo.Message, ex);
                    throw wrappedException;
                case ExceptionPolicyBehavior.Replace:
                    var newException = (Exception)Activator.CreateInstance(policyInfo.NewException, policyInfo.Message);
                    throw newException;
                case ExceptionPolicyBehavior.ReThrow:
                    throw ex;
                case ExceptionPolicyBehavior.Suppress:
                    break;
                default:
                    break;
            }
        }

        public void HandleException(Exception ex) {
            this.HandleException(ex, null);
        }

    }
}
