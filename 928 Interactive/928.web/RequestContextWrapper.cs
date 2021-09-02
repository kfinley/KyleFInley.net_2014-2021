using _928.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _928.Web {
    public class RequestContextWrapper : IRequestContext {

        public void SetRouteData(string key, object value) {
            HttpContext.Current.Request.RequestContext.RouteData.Values[key] = value;
        }

        public object RouteDataValue(string key) {
            return HttpContext.Current.Request.RequestContext.RouteData.Values[key];
        }

        public System.Security.Principal.IPrincipal User {
            get { return HttpContext.Current.User; }
        }

        public IDictionary Items {
            get { return HttpContext.Current.Items;  }
        }
    }
}
