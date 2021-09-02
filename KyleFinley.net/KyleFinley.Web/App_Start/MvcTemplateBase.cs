using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Web.WebPages.Instrumentation;
using System.Web.Mvc.Html;
using RazorEngine.Templating;
using RazorEngine.Text;
using System.Web.Routing;
using RazorEngine.Configuration;


namespace KyleFinley.Web
{

    //public static class RazorExtensions
    //{
    //    public static HelperResult RenderRazorContent(this HtmlHelper helper, string content)
    //    {
    //        return new HelperResult(writer =>
    //        {
    //            template(content).WriteTo(writer);
    //        });
    //    }
    //}

    public static class HtmlExtensions
    {
        public static string RenderContent(this HtmlHelper helper, string content, string key)
        {

            var config = new TemplateServiceConfiguration();
            config.Namespaces.Add("KyleFinley.Web");
            config.Namespaces.Add("System.Web.Mvc");
            config.Namespaces.Add("System.Web.Mvc.Html");

            config.EncodedStringFactory = new RawStringFactory();
            config.BaseTemplateType = typeof(MvcTemplateBase);

            var service = RazorEngineService.Create(config);
            HttpContext.Current.Items.Add("ControllerContext", helper.ViewContext.Controller.ControllerContext);

            return service.RunCompile(content, key);
        }
    }

    public class MvcTemplateBase : TemplateBase, IViewDataContainer
    {

        private HtmlHelper helper = null;
        private UrlHelper url = null;
        private ViewDataDictionary viewdata = null;
        private System.Dynamic.DynamicObject viewbag = null;

        public new dynamic ViewBag
        {
            get
            {
                var context = HttpContext.Current.Items["ControllerContext"] as ControllerContext;
                return context.Controller.ViewBag;
            }
        }

        public HtmlHelper Html
        {
            get
            {

                if (helper == null)
                {
                    var context = HttpContext.Current.Items["ControllerContext"] as ControllerContext;

                    var viewContext = new ViewContext(context, new FakeView(), context.Controller.ViewData, context.Controller.TempData, TextWriter.Null);
                    helper = new HtmlHelper(viewContext, new ViewPage());
                }
                return helper;
            }
        }

        public UrlHelper Url
        {
            get
            {
                if (url == null)
                {
                    //var p = WebPageContext.Current;
                    //var wvp = p.Page as WebViewPage;
                    //var context = wvp != null ? wvp.Request.RequestContext : null;

                    var httpContextBase = new HttpContextWrapper(HttpContext.Current);
                    var routeData = new RouteData();
                    var requestContext = new RequestContext(httpContextBase, routeData);

                    url = new UrlHelper(requestContext);
                    //url = new UrlHelper(context);
                }
                return url;
            }
        }

        public ViewDataDictionary ViewData
        {
            get
            {
                if (viewbag == null)
                {
                    var context = HttpContext.Current.Items["ControllerContext"] as ControllerContext;
                    viewdata = new ViewDataDictionary(context.Controller.ViewData);
                }

                return viewdata;
            }
            set
            {
                viewdata = value;
            }
        }
    }

    internal class FakeView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            throw new InvalidOperationException();
        }
    }
}
