using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Text.RegularExpressions;

using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Commands;
using KyleFinley.Web.App_Start;
using KyleFinley.Web.Controllers;
using KyleFinley.Commands;
using System.Web.Http;
using KyleFinley.Models;

namespace KyleFinley.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            IocBootstrapper.Run();
            MapperBootstrapper.Run();

            ModelBinderConfig.Configure();
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            var endResponse = false;
            var newUrl = string.Empty;

            if (Request.Url.Host.StartsWith("www") && !Request.Url.IsLoopback)
            {
                UriBuilder builder = new UriBuilder(Request.Url);
                builder.Host = Request.Url.Host.Replace("www.", "");
                Response.StatusCode = 301;
                newUrl = builder.ToString();
                Response.AddHeader("Location", newUrl);
                endResponse = true;
            }

            if (Request.Url.ToString().ToLower().Contains("/manage/"))
            {
                RouteTable.Routes.LowercaseUrls = false;
            }
            else {
                RouteTable.Routes.LowercaseUrls = true;

                if (Regex.IsMatch(Request.Url.ToString().Split('?')[0], @"[A-Z]"))
                {
                    Response.Clear();
                    Response.StatusCode = 301;
                    if (Request.Url.ToString().Contains('?'))
                    {
                        Response.AddHeader("Location", newUrl.HasValue() ? newUrl.Split('?')[0].ToLower() + '?' + newUrl.Split('?')[1]
                                                                         : Request.Url.ToString().Split('?')[0].ToLower() + '?' + Request.Url.ToString().Split('?')[1]);
                    }
                    else {
                        Response.AddHeader("Location", newUrl.HasValue() ? newUrl.ToLower() : Request.Url.ToString().ToLower());
                    }

                    endResponse = true;
                }
            }

            if (endResponse)
                Response.End();
        }

        protected void Application_Error(object sender, EventArgs e)
        {

            HttpApplication application = (HttpApplication)sender;
            var lastError = application.Server.GetLastError();
            //try {
            //    var exceptionHandler = new ExceptionHandler(new ExceptionHandlingPolicy(new PolicyInfo {
            //        OriginalException = lastError.GetType(),
            //        Behavior = ExceptionPolicyBehavior.Suppress,
            //        Message = lastError.Message,
            //        Data = new { Request = HttpContext.Current.Request.ToRaw() }
            //    }));

            //    exceptionHandler.HandleException(lastError, CommandFactory.Create<LogException>().Log);
            //} catch { }

#if DEBUG
            System.Diagnostics.Debug.Write(lastError.ToString());
#endif

            // If custom error is enabled
            if (HttpContext.Current.IsCustomErrorEnabled)
            {

                // Clear the error
                application.Server.ClearError();

                try
                {
                    application.Response.TrySkipIisCustomErrors = true;
                    application.Response.Clear();
                }
                catch (HttpException) { }

                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                if (lastError.GetType() == typeof(HttpException))
                {
                    // Determine whether 404 via HTTP code
                    if (lastError != null && ((HttpException)lastError).GetHttpCode() == 404)
                    {
                        routeData.Values.Add("action", "PageNotFound");
                        routeData.Values.Add("exception", lastError);
                    }
                }
                else {
                    routeData.Values.Add("action", "Error");
                    routeData.Values.Add("exception", lastError);
                }

                try
                {
                    IController controller = new ErrorController(new _928.Commands.CommandDispatcher(new _928.Web.HttpContextWrapper()));
                    controller.Execute(new RequestContext(new System.Web.HttpContextWrapper(Context), routeData));
                }
                catch (Exception ex)
                {
                    //TODO: Modify to log to somewhere besides the DB or do it without using Dependency Injection that can fail.

                    var handler = new ExceptionHandler(new ExceptionHandlingPolicy(
                        new PolicyInfo
                        {
                            OriginalException = ex.GetType(),
                            Behavior = ExceptionPolicyBehavior.Suppress,
                            Message = "Error in Global.asax Application_Error method. Message: {0}".FormatWith(ex.Message)
                        }));

                    handler.HandleException(ex, CommandFactory.Create<LogException>().Log);
                }
            }
        }

        protected void Application_PreSendRequestHeaders()
        {
            //Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }
    }
}
