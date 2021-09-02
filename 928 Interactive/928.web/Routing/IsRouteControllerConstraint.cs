using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Reflection;
using System.Web.Mvc;
using System.Web;
using System.Web.Caching;

namespace _928.Web.Routing
{
    public class IsRouteControllerConstraint : IRouteConstraint
    {
        private Dictionary<string, Type> _controllers;

        public IsRouteControllerConstraint()
        {

            _controllers = HttpContext.Current.Cache["Controller_Types"] as Dictionary<string, Type>;

            if (_controllers == null)
                   _controllers = Assembly
                                .GetCallingAssembly()
                                .GetTypes()
                                .Where(type => type.IsSubclassOf(typeof(Controller)))
                                .ToDictionary(key => key.Name.Replace("Controller", "").ToLower());

            HttpContext.Current.Cache.Add("Controller_Types", _controllers, null, Cache.NoAbsoluteExpiration, new TimeSpan(12, 0, 0), CacheItemPriority.Normal, null);
 
        }

        #region IRouteConstraint Members

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return _controllers.Keys.Contains((values["controller"] as string).ToLower());
        }

        #endregion
    }
}
