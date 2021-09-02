using System;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using _928.Core;
using _928.Commands;
using _928.Core.ExceptionHandling;


namespace _928.Web.Routing {
    public class HttpHandler : MvcHandler, IHttpHandler, IHttpAsyncHandler {

        public HttpHandler(RequestContext requestContext)
            : base(requestContext) {
        }

        void IHttpHandler.ProcessRequest(HttpContext context) {

            HandleRouting();
            base.ProcessRequest(context);
        }

        System.IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, System.AsyncCallback cb, object extraData) {
            HandleRouting();
            return base.BeginProcessRequest(context, cb, extraData);
        }

        void IHttpAsyncHandler.EndProcessRequest(System.IAsyncResult result) {
            base.EndProcessRequest(result);
        }

        bool IHttpHandler.IsReusable {
            get { return false; }
        }

        private void HandleRouting() {

            try {
                var url = RequestContext.RouteData.Values["url"].ToString();

                var urlHandler = CommandFactory.Create(UrlHandlerCommandType, new Tuple<string, object>("url", url));

                urlHandler.Execute();
            } catch (NullReferenceException ex) {
                throw new HttpException("Null Reference in HttpHandler. Likely cause is not setting a Url route parameter when defining root level route or a failure in Application_Start.", ex);
            }

        }
        public Type UrlHandlerCommandType { get; set; }

    }
}
