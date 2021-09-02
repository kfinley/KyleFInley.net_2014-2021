using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace _928.Web.Mvc
{
    public static class Extensions
    {

        public static string Action(this Controller controller)
        {
            return controller.ControllerContext.RouteData.Values["action"].ToString();
        }

        public static MvcHtmlString ActiveRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
        {
            return ActiveRouteLink(htmlHelper, linkText, routeName, routeValues, htmlAttributes, "active");
        }

        public static MvcHtmlString ActiveRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes, string activeClassName = "active")
        {
            return ActiveRouteLink(htmlHelper, linkText, routeName, routeValues, htmlAttributes, matchController: false, activeClassName: activeClassName);
        }

        public static MvcHtmlString ActiveRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes,
                                                    bool matchAction = false, string actionName = "", bool matchController = false, string controllerName = "", string activeClassName = "active")
        {
            var addActiveClass = false;

            if (matchController && htmlHelper.ViewContext.RouteData.GetRequiredString("controller") == controllerName)
            {
                addActiveClass = true;
            }
            else if (matchAction && htmlHelper.ViewContext.RouteData.GetRequiredString("action") == actionName)
            {
                addActiveClass = true;
            }
            else {
                var url = UrlHelper.GenerateUrl(routeName, null, null, null, null, null, new RouteValueDictionary(routeValues), htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, false);

                if (url != null && url.Equals(htmlHelper.ViewContext.HttpContext.Request.Url.PathAndQuery, StringComparison.OrdinalIgnoreCase))
                {
                    addActiveClass = true;
                }
            }

            if (addActiveClass)
            {
                return InternalActiveRouteLink(htmlHelper, linkText, routeName, routeValues, htmlAttributes, activeClassName);
            }

            return htmlHelper.RouteLink(linkText, routeName, routeValues, htmlAttributes);

        }

        private static MvcHtmlString InternalActiveRouteLink(HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes, string activeClassName)
        {
            var htmlAttributesDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var classAttribute = htmlAttributesDictionary.Where(a => a.Key == "class").FirstOrDefault();

            var classAttributeValue = classAttribute.Value as string;

            classAttributeValue = classAttributeValue + " " + activeClassName;

            htmlAttributesDictionary.Remove("class");
            htmlAttributesDictionary.Add("class", classAttributeValue.Trim());

            var lowerCaseRouteValues = new RouteValueDictionary();

            return htmlHelper.RouteLink(linkText, routeName, new RouteValueDictionary(routeValues), htmlAttributesDictionary);
        }
        
    }
}

