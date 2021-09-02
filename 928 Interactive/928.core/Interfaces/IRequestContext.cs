using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace _928.Core.Interfaces {
    public interface IRequestContext {
        
        object RouteDataValue(string key);
        void SetRouteData(string key, object value);
        IPrincipal User { get; }
        IDictionary Items { get; }
    }
}
