using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;

namespace _928.Web.Mvc.Security {
   
     public class SecureImageRouteHandler : IRouteHandler
    {
         public SecureImageRouteHandler() {
         }
        #region IRouteHandler Members

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SecureImageHttpHandler(requestContext) {
            };
        }
         
        #endregion
    }
}

