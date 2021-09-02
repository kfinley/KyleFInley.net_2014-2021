using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.ExceptionHandling {
    public class ExceptionHandlingPolicy {
        private Dictionary<Type, PolicyInfo> policies = new Dictionary<Type,PolicyInfo>();

        public ExceptionHandlingPolicy(params PolicyInfo[] policies) {
            foreach (var policy in policies) {
                this.policies.Add(policy.OriginalException, policy);
            }
        }

        public PolicyInfo PolicyInfoFor(Type t)
        {
            PolicyInfo policyInfo;

            try {
                policyInfo = policies[t];
            } catch (KeyNotFoundException) {
                policyInfo = policies[typeof(Exception)];
            }

            return policyInfo;
        }
    }
}
