using _928.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _928.Web.Mvc {

    public static class CustomLinkExtensions {

        public static LinkWrapper BeginRouteLinkIf(this HtmlHelper htmlHelper, bool displayLink, string routeName, object routeValues) {
            return BeginRouteLinkIf(htmlHelper, displayLink, string.Empty, routeName, new RouteValueDictionary(routeValues));
        }

        public static LinkWrapper BeginRouteLinkIf(this HtmlHelper htmlHelper, bool displayLink, string routeName, object routeValues, object htmlAttributes, bool lowerCaseUrl = true) {
            return BeginRouteLinkIf(htmlHelper, displayLink, string.Empty, routeName, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), lowerCaseUrl);
        }

        public static LinkWrapper BeginRouteLinkIf(this HtmlHelper htmlHelper, bool displayLink, string linkText, string routeName, object routeValues) {
            return BeginRouteLinkIf(htmlHelper, displayLink, linkText, routeName, new RouteValueDictionary(routeValues));
        }

        public static LinkWrapper BeginRouteLinkIf(this HtmlHelper htmlHelper, bool displayLink, string linkText, string routeName, RouteValueDictionary routeValues, bool lowerCaseUrl = true) {
            return BeginRouteLinkIf(htmlHelper, displayLink, linkText, routeName, routeValues, new RouteValueDictionary(), lowerCaseUrl);
        }

        public static LinkWrapper BeginRouteLinkIf(this HtmlHelper htmlHelper, bool displayLink, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool lowerCaseUrl) {

            if (displayLink) {
                return GenerateRouteLink(htmlHelper, htmlHelper.RouteCollection, linkText, routeName, routeValues, htmlAttributes, lowerCaseUrl);
            } else {
                return null;
            }

        }

        public static LinkWrapper GenerateRouteLink(HtmlHelper htmlHelper, RouteCollection routeCollection, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool lowerCaseUrl) {
            return GenerateRouteLink(htmlHelper, routeCollection, linkText, routeName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes, lowerCaseUrl);
        }

        public static LinkWrapper GenerateRouteLink(HtmlHelper htmlHelper, RouteCollection routeCollection, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool lowerCaseUrl) {
            return GenerateLinkInternal(htmlHelper, routeCollection, linkText, routeName, null /* actionName */, null /* controllerName */, protocol, hostName, fragment, routeValues, htmlAttributes, false /* includeImplicitMvcValues */, lowerCaseUrl);
        }

        private static LinkWrapper GenerateLinkInternal(HtmlHelper htmlHelper, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool includeImplicitMvcValues, bool lowerCaseUrl) {
            
            string url = UrlHelper.GenerateUrl(routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection, htmlHelper.ViewContext.RequestContext, includeImplicitMvcValues);

            var tagBuilder = new TagBuilder("a");
            tagBuilder.MergeAttributes(htmlAttributes);

            var link = new LinkWrapper(htmlHelper.ViewContext);
            if (lowerCaseUrl) {
                tagBuilder.MergeAttribute("href", url.ToLower());
            } else {
                tagBuilder.MergeAttribute("href", url);
            }

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            if (linkText != string.Empty) {
                htmlHelper.ViewContext.Writer.Write(linkText);
            }
            return link;
        }

        private static LinkWrapper LinkHelper(this HtmlHelper htmlHelper, string linkAddress, string linkText, IDictionary<string, object> htmlAttributes) {

            var tagBuilder = new TagBuilder("a");
            tagBuilder.MergeAttributes(htmlAttributes);

            var link = new LinkWrapper(htmlHelper.ViewContext);
            tagBuilder.MergeAttribute("href", linkAddress);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            if (linkText != string.Empty) {
                htmlHelper.ViewContext.Writer.Write(linkText);
            }
            return link;
        }

        public static void EndLink(this HtmlHelper htmlHelper) {
            EndLink(htmlHelper.ViewContext);
        }

        internal static void EndLink(ViewContext viewContext) {
            viewContext.Writer.Write("</a>");
            viewContext.OutputClientValidation();
            viewContext.FormContext = null;
        }
    }

    public class LinkWrapper : IDisposable {
        private readonly ViewContext viewContext;
        private bool disposed;

        public LinkWrapper(ViewContext viewContext) {
            Check.Argument.IsNotNull(viewContext, "viewContext");

            this.viewContext = viewContext;
        }

        public void Dispose() {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                disposed = true;
                CustomLinkExtensions.EndLink(viewContext);
            }
        }

        public void EndForm() {
            Dispose(true);
        }
    }


}
