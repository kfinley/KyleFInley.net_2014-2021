using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using _928.Core;
using System;
using _928.Commands;

namespace _928.Web.Routing {
    public class SecondLevelHttpHandler : MvcHandler, IHttpHandler, IHttpAsyncHandler {

        public SecondLevelHttpHandler(RequestContext requestContext)
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

            var topLevelVanityUrl = RequestContext.RouteData.Values["topLevelUrl"].ToString();
            var secondLevelVanityUrl = RequestContext.RouteData.Values["secondLevelUrl"].ToString();

            ICommand handleRouting;

            //TODO: REFACTOR THIS!  SHOULD BE A THIRDLEVELHTTPHANDLER!!
            if (RequestContext.RouteData.Values["itemIdentifier"] == null) {
                handleRouting = CommandFactory.Create(UrlHandlerCommandType,
                                                        new Tuple<string, object>("topLevelUrl", topLevelVanityUrl),
                                                        new Tuple<string, object>("secondLevelUrl", secondLevelVanityUrl),
                                                        new Tuple<string, object>("itemIdentifier", null));
            } else {
                var itemIdentifier = RequestContext.RouteData.Values["itemIdentifier"].ToString();

                handleRouting = CommandFactory.Create(UrlHandlerCommandType,
                                                        new Tuple<string, object>("topLevelUrl", topLevelVanityUrl),
                                                        new Tuple<string, object>("secondLevelUrl", secondLevelVanityUrl),
                                                        new Tuple<string, object>("itemIdentifier", itemIdentifier));
            }


            handleRouting.Execute();
        }

        public Type UrlHandlerCommandType { get; set; }

    }
}
