using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using _928.Core;

using _928.Web.Mvc;

namespace _928.Web.Routing {

    public class RouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new HttpHandler(requestContext) {
                UrlHandlerCommandType = UrlHandlerCommandType,
             };
        }

        public Type UrlHandlerCommandType { get; set; }

        #endregion
    }
}